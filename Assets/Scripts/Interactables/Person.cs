using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Person : AreaInteractable
{
    static public Dictionary<AttributeTypes, string> AttributeIconCodes = new Dictionary<AttributeTypes, string>();
    public enum AttributeTypes { Health, Energy, Hunger }

    public Dictionary<AttributeTypes, int> Attributes = new Dictionary<AttributeTypes, int>();

    [SerializeField]
    Transform playerCharacter;

    [SerializeField]
    public RadialProgressBar progressBar;

    [SerializeField]
    Sprite normalSprite;
    [SerializeField]
    Sprite hoveredSprite;
    [SerializeField]
    Sprite pickedUpSprite;

    [SerializeField]
    GameObject interactParticle;

    public int startHealth;
    public int startEnergy;
    public int startHunger;

    SpriteRenderer sr;

    public override void OnHover()
    {
        base.OnHover();
        if(!interacting)
        {
            sr.sprite = hoveredSprite;
        }
    }
    public override void OnUnhover()
    {
        base.OnUnhover();
        if(!interacting && !pickedUp)
        {
            sr.sprite = normalSprite;
        }

    }

    public override void OnPickup()
    {
        AudioManager.Play("Pickup");
        if(!interacting)
        {
            sr.sprite = pickedUpSprite;
        }
        base.OnPickup();
    }

    public override void SetInteracting(bool interacting)
    {
        if(interacting)
        {
            //LeanTween.value(gameObject, UpdateSpriteOpacity,1, 0, 0.2f);
        }
        else
        {
            //LeanTween.value(gameObject, UpdateSpriteOpacity, 0, 1, 0.2f);
        }
        base.SetInteracting(interacting);
    }

    void UpdateSpriteOpacity(float value)
    {
        Color oldColor = sr.color;
        oldColor.a = value;
        sr.color = oldColor;
    }

    public override void OnValidate()
    {
        base.OnValidate();
        GetComponent<SpriteRenderer>().sprite = normalSprite;
    }

    public override void Awake()
    {
        base.Awake();
        sr = GetComponent<SpriteRenderer>();
        Attributes.Add(AttributeTypes.Health, startHealth);
        Attributes.Add(AttributeTypes.Energy, startEnergy);
        Attributes.Add(AttributeTypes.Hunger, startHunger);
    }
    public override void Start()
    {
        base.Start();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        if(!pickedUp)
        {
            rb.MovePosition(Vector3.Lerp(transform.position, playerCharacter.position, followSpeed));
        }
    }

    public override void Interact(Area area)
    {
        base.Interact(area);
        Instantiate(interactParticle,transform.position,Quaternion.identity);
    }
    public override void TimePass(int amount)
    {
        base.TimePass(amount);
        Attributes[AttributeTypes.Hunger] -= amount;
        if(Attributes[AttributeTypes.Hunger] <= 0)
        {
            Die("starvation.");
        }
        if(Attributes[AttributeTypes.Health] <= 0)
        {
            Die("health decline.");
        }
    }

    public void Die(string reason)
    {
        Debug.Log(reason);
        LeanTween.cancelAll();
        GameEvents.instance.GameOver(reason);
        Destroy(gameObject);
    }
}
