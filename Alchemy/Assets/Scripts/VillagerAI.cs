using UnityEngine;
using UnityEngine.AI;

public class VillagerAI : MonoBehaviour
{
    public enum State { Walking, Waiting, Leaving }
    public State currentState = State.Walking;

    private Transform targetTransform; // Where the booth is
    public string requestedPotion;   // e.g., "Cure Sickness"

    //Where the villager goes to after getting the potion
    public Transform exitPoint;

    private NavMeshAgent agent;

    void Start()
{
    agent = GetComponent<NavMeshAgent>();

    // OPTION 1: Find by name (make sure the GameObject is named "VillagerTarget")
    GameObject targetObj = GameObject.Find("VillagerTarget");

    // OPTION 2: Find by tag (if you tagged it "BoothTarget")
    // GameObject targetObj = GameObject.FindGameObjectWithTag("BoothTarget");

    if (targetObj != null)
    {
        targetTransform = targetObj.transform;
        agent.SetDestination(targetTransform.position);
    }
    else
    {
        Debug.LogError("🚨 VillagerAI: Could not find VillagerTarget in scene!");
    }
}


    void Update()
    {
        if (currentState == State.Walking)
        {
            if (!agent.pathPending && agent.remainingDistance <= 0.2f)
            {
                currentState = State.Waiting;
                Debug.Log("Villager waiting for: " + requestedPotion);
            }
        }

        if (currentState == State.Leaving)
        {
            if (!agent.pathPending && agent.remainingDistance < 0.2f)
            {
                Debug.Log("Villager exited.");
                Destroy(gameObject); // or disable, or pool for reuse
            }
        }

    }

    public void ReceivePotion(string potionName)
    {
    if (potionName == requestedPotion)
    {
        Debug.Log("✅ Correct potion given!");
        // TODO: happy animation / effect / reward
    }
    else
    {
        Debug.Log("❌ Wrong potion!");
        // TODO: sad animation / boo sound
    }

    currentState = State.Leaving;
    agent.SetDestination(exitPoint.position);

}

}
