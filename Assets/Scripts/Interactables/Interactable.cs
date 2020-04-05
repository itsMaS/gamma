using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    enum Type { Person, Food, Medicine };

    [HideInInspector]
    public bool hoveredOver = false;
    [HideInInspector]
    public bool pickedUp = false;

    [HideInInspector]
    protected Vector3 cursorPosition;

    public void Hover()
    {
        if(!hoveredOver) { OnHover(); }
    }
    public void Unhover()
    {
        if (hoveredOver) { OnUnhover(); }
    }
    public void Pickup()
    {
        if (!pickedUp) { OnPickup(); }
    }
    public void LetGo()
    {
        if (pickedUp) { OnLetGo(); }
    }
    public void SetPointerWorldPosition(Vector3 position)
    {
        cursorPosition = position;
    }

    public virtual void OnHover()
    {
        hoveredOver = true;
    }
    public virtual void OnUnhover()
    {
        hoveredOver = false;
    }
    public virtual void OnPickup()
    {
        pickedUp = true;
    }
    public virtual void OnLetGo()
    {
        pickedUp = false;
    }

    public virtual void Interact(Area area)
    {

    }

    public virtual void FixedUpdate()
    {
        
    }
    public virtual void Update()
    {
        
    }
    public virtual void Awake()
    {
        
    }
    public virtual void Start()
    {
        
    }
    public virtual void OnValidate()
    {

    }
}
