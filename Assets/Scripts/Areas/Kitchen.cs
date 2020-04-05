using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kitchen : AreaRoom
{
    [SerializeField]
    int IngredientConsuptionAmount = 10;

    public int maxIngredients;
    public int maxMeals;


    public int ingredients = 0;
    public int meals = 0;
    public bool dishesWashed = true;

    public override void Start()
    {
        base.Start();
    }

    void UpdateInfo()
    {
        areaInfo.InfoItems.Clear();
        areaInfo.InfoItems.Add(new AreaInfo.Info("Ingredients", ingredients, maxIngredients));
        areaInfo.InfoItems.Add(new AreaInfo.Info("Meals", meals, maxMeals));
        areaInfo.UpdateInfo();
    }

    public override void Show()
    {
        base.Show();
        UpdateInfo();
    }

    public int CheckIngredients(int amount)
    {
        return Mathf.Max(0,amount + ingredients - maxIngredients);
    }
    public void AddIngredients(int amount)
    {
        ingredients = Mathf.Clamp(ingredients + amount, 0, maxIngredients);
    }

    public int CheckIngredientsConsumption()
    {
        return Mathf.Clamp(ingredients, 0, IngredientConsuptionAmount);
    }
    public void ConsumeIngredients()
    {
        ingredients -= CheckIngredientsConsumption();
    }
}
