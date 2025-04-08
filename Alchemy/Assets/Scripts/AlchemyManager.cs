using UnityEngine;
using System.Collections.Generic;

public class AlchemyManager : MonoBehaviour
{
    public static AlchemyManager Instance;

    private Dictionary<HashSet<string>, string> recipes = new();

    void Awake()
    {
        Instance = this;


        recipes.Add(new HashSet<string> { "Beer", "Horse Hair" }, "Strength");
        recipes.Add(new HashSet<string> { "Mushroom", "Son's Eye" }, "Confusion");
    }

    public string TryCombine(string a, string b)
    {
        foreach (var pair in recipes)
        {
            if (pair.Key.SetEquals(new[] { a, b }))
                return pair.Value;
        }
        return null;
    }

}
