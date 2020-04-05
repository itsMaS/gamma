using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalObject : Interactable
{
    [SerializeField]
    float dragOnPicked;
    [SerializeField]
    float dragNormal;

    protected Rigidbody2D rb;

    public override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody2D>();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        if(pickedUp)
        {
            rb.AddForce(GetBeamForce());
            rb.drag = dragOnPicked * PlayerController.DragOnDistance.Evaluate(Distance()) * 1.00f;
        }
    }
    public override void OnLetGo()
    {
        base.OnLetGo();
        rb.drag = dragNormal;
    }

    public override void OnValidate()
    {
        GetComponent<Rigidbody2D>().drag = dragNormal;
    }

    public Vector3 GetBeamForce()
    {
        return ((cursorPosition - transform.position).normalized *
            PlayerController.StrengthOnDistance.Evaluate(Distance()) *
            PlayerController.BeamStrength);
    }

    float Distance()
    {
        return Vector3.Distance(cursorPosition, transform.position);
    }
}
