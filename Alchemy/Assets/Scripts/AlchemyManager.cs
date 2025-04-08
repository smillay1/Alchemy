using UnityEngine;
using System.Collections.Generic;

public class AlchemyManager : MonoBehaviour
{
    public static AlchemyManager Instance;

    private Dictionary<HashSet<string>, string> recipes = new();
    public Dictionary<string, GameObject> resultPrefabs = new();


    //Add all of the potions/things that we make here:
    public GameObject strengthPrefab;
    public GameObject charismaPrefab;
    public GameObject fireGobletPrefab;
    public GameObject confusionPrefab;
    public GameObject couragePrefab;
    public GameObject demonMonsterPrefab;
    public GameObject cureSicknessPrefab;
    public GameObject rightLegLongerPrefab;
    public GameObject seeGodPrefab;
    public GameObject sicknessPrefab;



    void Awake()
    {
        Instance = this;


        recipes.Add(new HashSet<string> { "Beer", "Horse Hair" }, "Strength");
        resultPrefabs["Strength"] = strengthPrefab;

        recipes.Add(new HashSet<string> { "Beer", "Son's Eye" }, "Charisma");
        resultPrefabs["Charisma"] = charismaPrefab;

        recipes.Add(new HashSet<string> { "Beer", "Dragon's Egg" }, "FireGoblet");
        resultPrefabs["FireGoblet"] = fireGobletPrefab;

        recipes.Add(new HashSet<string> { "Beer", "Mushroom" }, "Confusion");
        resultPrefabs["Confusion"] = confusionPrefab;

        recipes.Add(new HashSet<string> { "Horse Hair", "Son's Eye" }, "Courage");
        resultPrefabs["Courage"] = couragePrefab;

        recipes.Add(new HashSet<string> { "Horse Hair", "Dragon's Egg" }, "DemonMonster");
        resultPrefabs["DemonMonster"] = demonMonsterPrefab;

        recipes.Add(new HashSet<string> { "Horse Hair", "Mushroom" }, "CureSickness");
        resultPrefabs["CureSickness"] = cureSicknessPrefab;

        recipes.Add(new HashSet<string> { "Son's Eye", "Dragon's Egg" }, "RightLegLonger");
        resultPrefabs["RightLegLonger"] = rightLegLongerPrefab;

        recipes.Add(new HashSet<string> { "Son's Eye", "Mushroom" }, "SeeGod");
        resultPrefabs["SeeGod"] = seeGodPrefab;

        recipes.Add(new HashSet<string> { "Dragon's Egg", "Mushroom" }, "Sickness");
        resultPrefabs["Sickness"] = sicknessPrefab;



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
