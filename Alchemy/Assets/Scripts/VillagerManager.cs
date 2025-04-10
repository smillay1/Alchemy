using UnityEngine;

public class VillagerManager : MonoBehaviour
{
    public GameObject villagerPrefab;
    public Transform spawnPoint;
    public Transform boothPoint;
    public Transform exitPoint;

    public string[] possiblePotions = { "Strength", "Confusion", "Cure Sickness", "Charisma", "Fire Goblet", "Courage"};

    private VillagerAI currentVillager;

    void Start()
    {
        SpawnNextVillager();
    }

    public VillagerAI GetCurrentVillager()
{
    return currentVillager;
}


    public void SpawnNextVillager()
    {
        GameObject villagerGO = Instantiate(villagerPrefab, spawnPoint.position, Quaternion.identity);
        VillagerAI villager = villagerGO.GetComponent<VillagerAI>();

        villager.targetTransform = boothPoint;
        villager.exitPoint = exitPoint;

        villager.requestedPotion = possiblePotions[Random.Range(0, possiblePotions.Length)];
        villager.manager = this;

        currentVillager = villager;
    }
}
