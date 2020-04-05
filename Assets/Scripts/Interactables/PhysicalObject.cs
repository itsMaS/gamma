using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalObject : Interactable
{
    [SerializeField]
    float followSpeed = 0.2f;
    [SerializeField]
    float dragOnPicked;
    [SerializeField]
    float dragNormal;

    protected Rigidbody2D rb;
    Vector3 velocity;

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
            rb.MovePosition(Vector3.Lerp(transform.position,cursorPosition,followSpeed));
        }
    }
    public override void OnLetGo()
    {
        base.OnLetGo();
        rb.drag = dragNormal;
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
