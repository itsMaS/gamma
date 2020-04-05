using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outside : AreaRoom
{
    [SerializeField]
    Transform dropPoint;

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

    public void SpawnIngredients(int count)
    {
        StartCoroutine(SpawnRandom(count));
    }

    IEnumerator SpawnRandom(int count)
    {
        for (int i = 0; i < count; i++)
        {
            SpawnIngredient(dropPoint.position + new Vector3(Random.Range(-5,5), Random.Range(-5, 5), 0));
            yield return new WaitForSeconds(0.3f);
        }
    }
}
