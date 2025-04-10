using UnityEngine;
using UnityEngine.AI;

public class VillagerAI : MonoBehaviour
{
    public enum State { Walking, Waiting, Leaving }
    public State currentState = State.Walking;

     // Where the booth is
    public Transform targetTransform;

    // The potion the villager is requesting
    public string requestedPotion;

    //Where the villager goes to after getting the potion
    public Transform exitPoint;

    private NavMeshAgent agent;

    public VillagerManager manager;


    void Start()
{
    agent = GetComponent<NavMeshAgent>();

    agent.SetDestination(targetTransform.position);

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
                manager.SpawnNextVillager();
                Destroy(gameObject);
            }
        }


    }

    public void ReceivePotion(string potionName)
    {
    if (potionName == requestedPotion)
    {
        Debug.Log("Correct potion");

    }
    else
    {
        Debug.Log("Wrong potion");
    }

    currentState = State.Leaving;
    agent.SetDestination(exitPoint.position);

}

}
