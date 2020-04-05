using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "KitchenAction", menuName = "Actions/Kitchen", order = 1)]
public class KitchenAction : PersonAction
{
    public static int mealCost = 10;

    enum Type{ WashDishes, EatMeal, EatIngredients, PrepareMeal, AddIngredients };

    [SerializeField]
    Type actionType;

    Kitchen room;

    public override void Initialize(AreaRoom parent, AreaAction child)
    {
        base.Initialize(parent, child);
        room = parent as Kitchen;
        if(!room)
        {
            Debug.LogError("Action is placed in the wrong room! [Not kitchen]");
        }
    }

    public override void ActionFinished()
    {
        base.ActionFinished();
        switch (actionType)
        {
            case Type.WashDishes:
                room.dishesWashed = true;
                break;
            case Type.EatMeal:
                room.dishesWashed = false;
                break;
            case Type.EatIngredients:
                room.ConsumeIngredients();
                break;
            case Type.PrepareMeal:
                room.meals++;
                room.ingredients -= mealCost;
                break;
            case Type.AddIngredients:
                room.AddIngredients((currentInteractable as FoodIngredient).nutritionValue);
                Destroy(currentInteractable.gameObject);
                break;
            default:
                break;
        }
    }

    public override bool Doable(AreaInteractable interactable, out string message)
    {
        switch (actionType)
        {
            case Type.WashDishes:
                if (room.dishesWashed)
                {
                    message = string.Format($"<color={GameAction.errorColor}>Dishes are already washed.");
                    return false;
                }
                break;
            case Type.EatMeal:
                if(room.meals <= 0)
                {
                    message = string.Format($"<color={GameAction.errorColor}>There are no meals.");
                    return false;
                }
                if (!room.dishesWashed)
                {
                    message = string.Format($"<color={GameAction.errorColor}>All the dishes are dirty.");
                    return false;
                }
                break;
            case Type.EatIngredients:
                AttributeValues.RemoveAll(e => (e.type == Person.AttributeTypes.Hunger));
                Required req = new Required();
                req.type = Person.AttributeTypes.Hunger;
                req.netAmount = room.CheckIngredientsConsumption();

                if(req.netAmount <= 0)
                {
                    message = string.Format($"<color={GameAction.errorColor}>No ingredients in the kitchen.");
                    return false;
                }

                AttributeValues.Add(req);
                break;
            case Type.PrepareMeal:
                if(room.meals >= room.maxMeals)
                {
                    message = string.Format($"<color={GameAction.errorColor}>Meals are at full capacity. [{room.meals}/{room.maxMeals}]");
                }
                if (room.ingredients < mealCost)
                {
                    message = string.Format($"<color={GameAction.errorColor}>Not enough ingredients for a meal. [{room.ingredients}/{mealCost}]");
                    return false;
                }
                break;
            case Type.AddIngredients:
                FoodIngredient ingredient = interactable as FoodIngredient;
                if (ingredient)
                {
                    int waste;
                    if ((waste = room.CheckIngredients(ingredient.nutritionValue)) > 0)
                    {
                        if(ingredient.nutritionValue - waste <= 0)
                        {
                            message = string.Format($"<color={GameAction.errorColor}>Full food ingredient capacity [{room.ingredients}/{room.maxIngredients}]");
                            return false;
                        }

                        message = string.Format($"<color={GameAction.okColor}>+{ingredient.nutritionValue-waste} ingredients.");
                        message += string.Format($"<color={GameAction.errorColor}> \n -{waste} Wasted");
                    }
                    else
                    {
                        message = string.Format($"<color={GameAction.okColor}>+{ingredient.nutritionValue} food ingredients.");
                    }
                    return true;
                }
                break;
            default:
                break;
        }
        return base.Doable(interactable, out message);
    }
    public override void Interact(AreaInteractable interactable)
    {
        base.Interact(interactable);
    }
}
