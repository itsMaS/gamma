using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class GameAction : ScriptableObject
{
    protected AreaInteractable currentInteractable;

    public static string errorColor;
    public static string okColor;

    public string doingPhrase;
    public int time;

    public abstract bool Doable(AreaInteractable interactable, out string message);
    public virtual void Interact(AreaInteractable interactable)
    {
        currentInteractable = interactable;
        if (time >= 0)
        {
            interactable.SetInteracting(true);
            GameManager.instance.StartTime(time, ActionFinished);
        }
        else
        {
            ActionFinished();
        }
    }
    public virtual void Initialize(AreaRoom parent, AreaAction child) { }

    public virtual void ActionFinished()
    {
        currentInteractable.SetInteracting(false);
        currentInteractable = null;
    }
}
