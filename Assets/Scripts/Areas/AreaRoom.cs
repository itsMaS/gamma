using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaRoom : Area
{
    [SerializeField]
    protected AreaInfo areaInfo;

    [SerializeField]
    Transform actions;

    [SerializeField]
    string RoomName;

    List<AreaAction> Actions = new List<AreaAction>();

    public override void OnValidate()
    {
        base.OnValidate();
        AreaVisual av = GetComponent<AreaVisual>();
        foreach (Transform action in actions)
        {
            action.GetComponent<AreaVisual>().SetColor(av.areaColor);
        }
        av.SetAreaName(RoomName);

    }

    public override bool Accept(AreaInteractable item, out string message)
    {
        message = "";
        return false;

        return base.Accept(item, out message);
    }

    public override void Start()
    {
        base.Start();

        GameEvents.instance.OnShowActions += OnShow;
        GameEvents.instance.OnHideAreas += OnHide;

        foreach (Transform action in actions)
        {
            Actions.Add(action.GetComponent<AreaAction>());
            action.gameObject.SetActive(false);
        }
    }
    public virtual void OnShow(AreaInteractable interactable)
    {
        bool areaUsed = false;
        foreach (var item in Actions)
        {
            foreach (var group in interactable.ActionGroups)
            {
                if (group.AvailableActions.Contains(item.action))
                {
                    item.EnableSelf();
                    item.action.Initialize(this,item);
                    item.UpdateLayout();
                    areaUsed = true;
                }
            }
        }
        if (areaUsed)
        {
            Show();
        }
    }
    public virtual void OnHide()
    {
        foreach (var item in Actions)
        {
            item.DisableSelf();
        }
        Hide();
    }
    public override bool Hover(AreaInteractable item)
    {
        if(areaInfo)
        {
            areaInfo.Show();
        }
        foreach (var action in Actions)
        {
            if(item.gameObject.activeInHierarchy)
            {
                action.Show();
            }
        }
        an.SetBool("Empty",true);
        return true;
    }

    public override void Unhover()
    {
        if (areaInfo)
        {
            areaInfo.Hide();
        }
        foreach (Transform action in actions)
        {
            action.GetComponent<AreaAction>().Hide();
        }
        an.SetBool("Empty", false);
        base.Unhover();
    }
}
