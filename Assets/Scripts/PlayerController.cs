using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    static PlayerController instance;

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

    [SerializeField]
    float cameraFollowSpeed = 0.2f;
    Vector3 cameraTarget = new Vector3();

    [SerializeField]
    Vector2 HorizontalClamp;
    [SerializeField]
    Vector2 VerticalClamp;
    [SerializeField]
    float mouseInfluence = 1;

    [SerializeField]
    AnimationCurve cursorMoveCof;

    Animator an;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(new Vector3(Mathf.Lerp(HorizontalClamp.x, HorizontalClamp.y, 0.5f),
            Mathf.Lerp(VerticalClamp.x, VerticalClamp.y, 0.5f)),
            new Vector3(Mathf.Abs(HorizontalClamp.y - HorizontalClamp.x), Mathf.Abs(VerticalClamp.y - VerticalClamp.x)));
    }

    private void Awake()
    {
        instance = this;
        an = GetComponent<Animator>();
        cam = FindObjectOfType<Camera>();
    }

    public static void Shake(int magnitude)
    {
        instance.an.SetInteger("Magnitude",magnitude);
        instance.an.SetTrigger("Shake");
    }

    private void Update()
    {
        cameraTarget += cameraSensitivity * new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        if(heldIneractable)
        {
            cameraTarget += mouseInfluence * new Vector3(Magnitude(true, out float mag1)*mag1,Magnitude(false, out float mag2)*mag2);
        }
        cameraTarget.x = Mathf.Clamp(cameraTarget.x, HorizontalClamp.x, HorizontalClamp.y);
        cameraTarget.y = Mathf.Clamp(cameraTarget.y, VerticalClamp.x, VerticalClamp.y);


        transform.position = Vector3.Lerp(transform.position,cameraTarget,cameraFollowSpeed);
        cursorWorldPosition = Vector3.ProjectOnPlane(cam.ScreenToWorldPoint(Input.mousePosition), Vector3.forward);
    }

    static Vector3 normalizedMouse()
    {
        float x = Input.mousePosition.x / Screen.width - 0.5f;
        float y = Input.mousePosition.y / Screen.height - 0.5f;
        return new Vector2(x,y);
    }
    float Magnitude(bool horizontal, out float magnitude)
    {
        if (horizontal)
        {
            float x = Input.mousePosition.x / Screen.width - 0.5f;
            magnitude = cursorMoveCof.Evaluate(2*Mathf.Abs(x));
            return x;
        }
        else
        {
            float y = Input.mousePosition.y / Screen.height - 0.5f;
            magnitude = cursorMoveCof.Evaluate(2*Mathf.Abs(y));
            return y;
        }
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
