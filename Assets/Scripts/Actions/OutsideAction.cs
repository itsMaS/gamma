using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "OutsideAction", menuName = "Actions/Outside", order = 1)]
public class OutsideAction : PersonAction
{
    enum Type { ScavengeForFood, ScavengeForMedicine };

    [SerializeField]
    Type actionType;


    Outside room;

    public override void Initialize(AreaRoom parent, AreaAction child)
    {
        base.Initialize(parent, child);
        room = parent as Outside;
        if (!room)
        {
            Debug.LogError("Action is placed in the wrong room! [Not outside]");
        }
    }

    public override void Interact(AreaInteractable interactable)
    {
        switch (actionType)
        {
            case Type.ScavengeForFood:
                room.SpawnIngredients(3,interactable.transform.position);
                break;
            case Type.ScavengeForMedicine:
                break;
            default:
                break;
        }
        base.Interact(interactable);
    }
}
