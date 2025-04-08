using UnityEngine;
using System.Collections.Generic;

public class Cauldron : MonoBehaviour
{
    public List<string> ingredientsInPot = new List<string>();

    private void OnTriggerEnter(Collider other)
    {
        Ingredient ingredient = other.GetComponent<Ingredient>();
        if (ingredient != null && !ingredientsInPot.Contains(ingredient.ingredientName))
        {
            ingredientsInPot.Add(ingredient.ingredientName);
            Destroy(other.gameObject); // Removes the ingredient

            if (ingredientsInPot.Count == 2)
            {
                CombineIngredients();
            }
        }
    }

    void CombineIngredients()
    {
    string a = ingredientsInPot[0];
    string b = ingredientsInPot[1];
    Debug.Log($"Trying to combine: {a} + {b}");

    string result = AlchemyManager.Instance.TryCombine(a, b);
    if (result != null)
    {
        Debug.Log("Created: " + result);

        // Spawn the result next to the cauldron
        if (AlchemyManager.Instance.resultPrefabs.TryGetValue(result, out GameObject prefab))
        {
            //Position of the resulting potion
            Vector3 spawnPos = transform.position + transform.right * 1.2f;
            Instantiate(prefab, spawnPos, Quaternion.identity);
        }
    }

    else
    {
        Debug.Log("Nothing happened.");
    }

    ingredientsInPot.Clear();
    }

}
