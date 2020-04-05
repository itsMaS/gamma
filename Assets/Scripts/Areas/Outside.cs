using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outside : AreaRoom
{
    [SerializeField]
    GameObject[] Ingredients;
    [SerializeField]
    GameObject[] Meals;

    public void SpawnIngredient(Vector3 position)
    {
        Instantiate(Ingredients[Random.Range(0,Ingredients.Length)],position,Quaternion.identity);
    }
    public void SpawnMeal(Vector3 position)
    {
        Instantiate(Meals[Random.Range(0, Meals.Length)], position, Quaternion.identity);
    }

    public void SpawnIngredients(int count, Vector3 location)
    {
        StartCoroutine(SpawnRandom(count,location));
    }

    IEnumerator SpawnRandom(int count, Vector3 spawnLocation)
    {
        for (int i = 0; i < count; i++)
        {
            SpawnIngredient(spawnLocation + new Vector3(Random.Range(-20,20), Random.Range(-20, 20), 0));
            yield return new WaitForSeconds(0.3f);
        }
    }
}
