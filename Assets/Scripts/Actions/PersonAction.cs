using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SimpleAction", menuName = "Actions/Simple", order = 1)]
public class PersonAction : GameAction
{
    [System.Serializable]
    public struct Required
    {
        public Person.AttributeTypes type;
        public int netAmount;
    }

    [SerializeField]
    protected List<Required> AttributeValues = new List<Required>();

    [Multiline]
    [SerializeField]
    string actionDescription;
    string lastNetResources;

    public override bool Doable(AreaInteractable interactable , out string message)
    {
        currentInteractable = interactable;
        Person person = interactable as Person;
        if(person)
        {
            bool check = true;
            message = "";
            foreach (var item in AttributeValues)
            {
                if(!MakeString(ref message, person.Attributes[item.type],item.netAmount,Person.AttributeIconCodes[item.type]))
                {
                    check = false;
                }
            }
            lastNetResources = message;
            message += string.Format($"<color={GameAction.okColor}> {time}{"<sprite=3>"}   ");


            if (!check)
            {
                message = string.Format($"<color={GameAction.okColor}>You're missing resources! \n {message}") ;
                return false;
            }
            else
            {
                message = string.Format($"<color={GameAction.okColor}>{actionDescription}\n") + message;
            }
            return true;
        }
        else
        {
            message = string.Format($"<color={GameAction.errorColor}>Object is not a person");
            return false;
        }
    }

    public override void Interact(AreaInteractable interactable)
    {
        base.Interact(interactable);
        GameManager.MakePopup(lastNetResources, interactable.transform.position);
        Person person = interactable as Person;
        if(person)
        {
            person.progressBar.StartCount(time,this);
            foreach (var item in AttributeValues)
            {
                person.Attributes[item.type] = Mathf.Clamp(person.Attributes[item.type] + item.netAmount, 0,100);
            }
        }
    }

    bool MakeString(ref string mesg, float person, float netValue, string iconCode)
    {
        if(netValue > 0)
        {
            mesg += string.Format($"<color={GameAction.okColor}> +{netValue}{iconCode}   ");
            return true;
        }
        else if(netValue < 0)
        {
            if(person >= -netValue)
            {
                mesg += string.Format($"<color={GameAction.okColor}> {netValue}{iconCode}   ");
                return true;
            }
            else
            {
                mesg += string.Format($"<color={GameAction.errorColor}> {netValue}{iconCode}   ");
                return false;
            }
        }
        return true;
    }
}
