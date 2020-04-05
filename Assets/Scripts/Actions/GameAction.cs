using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class GameAction : ScriptableObject
{
    public static string errorColor;
    public static string okColor;

    public abstract bool Doable(AreaInteractable interactable, out string message);
    public abstract void Interact(AreaInteractable interactable);
    public virtual void Initialize(AreaRoom parent, AreaAction child) { }
}
