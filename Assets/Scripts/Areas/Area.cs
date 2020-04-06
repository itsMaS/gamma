using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Area : MonoBehaviour
{
    GameObject area;
    BoxCollider2D box;
    protected Animator an;
    [HideInInspector]
    public AreaVisual visual;


    public virtual void Awake()
    {
        area = transform.GetChild(0).gameObject;
        box = GetComponent<BoxCollider2D>();
        an = GetComponent<Animator>();
        visual = GetComponent<AreaVisual>();
    }

    public virtual void Start()
    {
        Hide();
        DisableGameObject();
    }
    public virtual bool Hover(AreaInteractable item)
    {
        an.SetBool("Hover", true);
        if (an.GetBool("Hover"))
        {

        }
        if (Accept(item, out string mesg))
        {
            visual.SetAreaMessage(mesg);
            HoverAccept();
            return true;
        }
        else
        {
            visual.SetAreaMessage(mesg);
            HoverDeny();
            return false;
        }
    }
    public virtual void Unhover()
    {
        an.SetBool("Hover", false);
    }
    public void Interact(AreaInteractable item)
    {
        if (Accept(item, out string no))
        {
            Interacted(item);
            item.Interact(this);
        }
        else
        {
            InteractDeny();
        }
    }
    public virtual void Interacted(AreaInteractable item)
    {
    }

    public virtual bool Accept(AreaInteractable item, out string message)
    {
        message = "";
        return true;
    }
    public virtual void HoverAccept()
    {
    }
    public virtual void HoverDeny()
    {
        if (!an.GetBool("Hover"))
        {
            visual.PingText();
        }
    }

    public virtual void InteractDeny()
    {
        PlayerController.Shake(1);
        AudioManager.Play("Deny",0.8f);
        GameManager.MakePopup(string.Format($"<color={GameAction.errorColor}> Action cannot be done."),transform.position);
        visual.PingText();
    }

    public virtual void DisableGameObject()
    {
        area.SetActive(false);
    }

    public virtual void EnableGameObject()
    {
        area.SetActive(true);
    }

    public virtual void Show()
    {
        an.SetBool("Show", true);
        box.enabled = true;
        area.SetActive(true);
    }
    public virtual void Hide()
    {
        an.SetBool("Show", false);
        box.enabled = false;
    }

    public virtual void OnValidate()
    {

    }
}
