using UnityEngine;
using System.Collections.Generic;


public class VillagerManager : MonoBehaviour
{
    public GameObject villagerPrefab;
    
    public Transform spawnPoint;
    public Transform boothPoint;
    public Transform exitPoint;

    public string[] possiblePotions = { "Strength", "Confusion", "Cure Sickness", "Charisma", "Fire Goblet", "Courage"};

    private VillagerAI currentVillager;

    public List<VillagerAI> allVillagers = new List<VillagerAI>();
    private int currentIndex = 0;


    void Start()
    {
    for (int i = 0; i < 4; i++) // spawn 4 villagers
    {
        SpawnNextVillager();
    }

    Invoke(nameof(SendNextVillagerToBooth), 1f); // call first one after delay
    }


    public VillagerAI GetCurrentVillager()
    {
        return currentVillager;
    }

    public void RegisterVillager(VillagerAI villager)
    {
        allVillagers.Add(villager);
    }

    public void SendNextVillagerToBooth()
    {
        if (currentIndex >= allVillagers.Count) return;

        VillagerAI villager = allVillagers[currentIndex];
        currentVillager = villager;

        villager.currentState = VillagerAI.State.Walking;
        villager.agent.SetDestination(villager.targetTransform.position); // booth
        currentIndex++;
    }



    public void SpawnNextVillager()
    {
        GameObject villagerGO = Instantiate(villagerPrefab, spawnPoint.position, Quaternion.identity);
        VillagerAI villager = villagerGO.GetComponent<VillagerAI>();

        villager.targetTransform = boothPoint;
        villager.exitPoint = exitPoint;

        villager.requestedPotion = possiblePotions[Random.Range(0, possiblePotions.Length)];
        villager.manager = this;
        villager.currentState = VillagerAI.State.Idle;


        currentVillager = villager;
    }
}
