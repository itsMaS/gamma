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
    GameObject interactParticle;

    public int startHealth;
    public int startEnergy;
    public int startHunger;

    public override void Awake()
    {
        base.Awake();
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
            Debug.Log("Death of starvation");
        }
    }
}
