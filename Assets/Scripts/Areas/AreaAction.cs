using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AreaAction : Area
{
    [SerializeField]
    Image img;

    bool interacted = false;

    static AreaAction current;
    public GameAction action;

    bool hovered;

    public override void OnValidate()
    {
        base.OnValidate();
        if (action != null)
        {
            GetComponent<AreaVisual>().SetAreaName(action.name);
            gameObject.name = action.name;
        }
        SetLayout();
    }

    public override void Start()
    {
        base.Start();
    }

    void SetLayout()
    {
        visual = GetComponent<AreaVisual>();
        RectTransform rect = GetComponent<RectTransform>();

        visual.SetDimensions(rect.position, rect.sizeDelta.x, rect.sizeDelta.y);
    }

    public void UpdateLayout()
    {
        StartCoroutine(SetLayoutWait());
    }

    public override bool Accept(AreaInteractable item, out string message)
    {
        return action.Doable(item, out message);
    }

    public override void Interacted(AreaInteractable item)
    {
        base.Interacted(item);
        interacted = true;
        action.Interact(item);
    }

    public override bool Hover(AreaInteractable item)
    {

        visual.InflateMessage();
        if (current && this != current)
        {
            current.Unhover();
        }
        current = this;

        return base.Hover(item);
    }
    public override void Unhover()
    {
        visual.DeflateMessage();
        base.Unhover();
    }
    public override void HoverDeny()
    {
        base.HoverDeny();
    }

    public void DisableSelf()
    {
        gameObject.SetActive(false);

        /*
        if (interacted)
        {
            GetComponent<LayoutElement>().ignoreLayout = true;
            StartCoroutine(DelayDisable());
        }
        else
        {
            GetComponent<LayoutElement>().ignoreLayout = false;
            gameObject.SetActive(false);
        }
        */
    }
    public void EnableSelf()
    {
        GetComponent<LayoutElement>().ignoreLayout = false;
        gameObject.SetActive(true);
    }
    IEnumerator SetLayoutWait()
    {
        yield return null;
        yield return new WaitForEndOfFrame();
        SetLayout();
    }
    IEnumerator DelayDisable()
    {
        yield return new WaitForSecondsRealtime(2);
        interacted = false;
        DisableGameObject();
    }
}
