using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BedroomAction", menuName = "Actions/Bedroom", order = 1)]
public class BetroomAction : PersonAction
{
    public int sleepAmount;
    Bedroom room;

    public override void Initialize(AreaRoom parent, AreaAction child)
    {
        base.Initialize(parent, child);
        room = parent as Bedroom;
        if (!room)
        {
            Debug.LogError("Action is placed in the wrong room! [Not bedroom]");
        }
    }

    public override bool Doable(AreaInteractable interactable, out string message)
    {
        Person person = interactable as Person;
        if(person && person.Attributes[Person.AttributeTypes.Energy] > 50)
        {
            message = string.Format($"<color={GameAction.errorColor}>You are not tired [<sprite=1>>50]");
            return false;
        }
        return base.Doable(interactable, out message);
    }
    public override void Interact(AreaInteractable interactable)
    {
        base.Interact(interactable);
    }
}
