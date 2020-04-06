using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaInteractable : PhysicalObject
{
    [Header("Available Actions")]
    public List<ActionGroup> ActionGroups;

    Area insideArea;
    protected bool interacting = false;

    public virtual void SetInteracting(bool interacting)
    {
        this.interacting = interacting;
    }

    public override void OnPickup()
    {
        if(!interacting)
        {
            base.OnPickup();
            AudioManager.Play("Pickup",0.5f);
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

    public override void Interact(Area area)
    {
        AudioManager.PlayWithPitchDeviation("Insert",0.8f,0.3f);
        PlayerController.Shake(2);
        base.Interact(area);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(PlayerController.heldIneractable == this)
        {
            Area area = collision.GetComponent<Area>();
            if (area as AreaAction)
            {
                insideArea = area;
            }
            area.Hover(this);

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (PlayerController.heldIneractable == this)
        {
            Area area = collision.GetComponent<Area>();
            area.Unhover();
            if(area as AreaAction && insideArea == area)
            {
                insideArea = null;
            }
        }
    }
}
