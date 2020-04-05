using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static Interactable heldIneractable;
    public static Interactable hoveredIneractable;

    public static float BeamStrength;
    public static AnimationCurve StrengthOnDistance;
    public static AnimationCurve DragOnDistance;

    [Header("Player settings")]
    [SerializeField]
    LayerMask interactionMask;
    [SerializeField]
    float beamStrength;
    [SerializeField]
    AnimationCurve strengthOnDistance;
    [SerializeField]
    AnimationCurve dragOnDistance;
    [SerializeField]
    float cameraSensitivity = 1;

    Camera cam;

    Interactable current;
    Interactable next;

    Vector3 cursorWorldPosition;

    private void Awake()
    {
        cam = FindObjectOfType<Camera>();
    }

    private void Update()
    {
        cam.transform.Translate(cameraSensitivity*new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"),0));
        cursorWorldPosition = Vector3.ProjectOnPlane(cam.ScreenToWorldPoint(Input.mousePosition), Vector3.forward);
    }

    private void FixedUpdate()
    {
        SetPlayerSettings();

        RaycastHit2D hit = Physics2D.Raycast(cursorWorldPosition, Vector2.zero,100,interactionMask);
        if (!heldIneractable && hit.collider != null && (next = hit.collider.GetComponent<Interactable>()))
        {
            if(current)
            {
                current.Unhover();
            }
            next.Hover();

            if(Input.GetMouseButton(0))
            {
                next.Pickup();
                heldIneractable = next;
            }
            current = next;
        }
        else
        {
            if(current)
            {
                current.Unhover();
            }
        }
        if(!Input.GetMouseButton(0) && heldIneractable)
        {
            current.LetGo();
            heldIneractable = null;
        }

        if(current && heldIneractable)
        {
            current.SetPointerWorldPosition(cursorWorldPosition);
        }
    }

    void SetPlayerSettings()
    {
        BeamStrength = beamStrength;
        StrengthOnDistance = strengthOnDistance;
        DragOnDistance = dragOnDistance;
    }
}
