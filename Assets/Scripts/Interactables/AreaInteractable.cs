﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaInteractable : PhysicalObject
{
    [Header("Available Actions")]
    public List<ActionGroup> ActionGroups;

    Area insideArea;
    bool interacting = false;

    public void SetInteracting(bool interacting)
    {
        this.interacting = interacting;
    }

    public override void OnPickup()
    {
        if(!interacting)
        {
            base.OnPickup();
            GameEvents.instance.ShowActions(this);
        }
    }
    public override void OnLetGo()
    {
        if (insideArea)
        {
            insideArea.Interact(this);
        }
        base.OnLetGo();
        GameEvents.instance.HideAreas();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(PlayerController.heldIneractable == this)
        {
            insideArea = collision.GetComponent<Area>();
            insideArea.Hover(this);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (PlayerController.heldIneractable == this)
        {

            collision.GetComponent<Area>().Unhover();
        }
    }
}
