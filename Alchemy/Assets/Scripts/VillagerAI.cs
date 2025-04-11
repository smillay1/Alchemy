using UnityEngine;
using UnityEngine.AI;

public class VillagerAI : MonoBehaviour
{
    public enum State { Idle, Walking, Waiting, Leaving }

    public State currentState = State.Walking;

     // Where the booth is
    public Transform targetTransform;

    // The potion the villager is requesting
    public string requestedPotion;

    //Where the villager goes to after getting the potion
    public Transform exitPoint;

    public NavMeshAgent agent;


    public VillagerManager manager;

    private float wanderRadius = 5f;
    private float wanderTimer = 5f;
    private float wanderCooldown;



    void Start()
    {
    manager.RegisterVillager(this);

    agent = GetComponent<NavMeshAgent>();
    agent.updateRotation = false;


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

        if (agent.velocity.magnitude > 0.1f)
        {
            transform.forward = agent.velocity.normalized;
        }


        if (currentState == State.Idle)
        {
            wanderCooldown -= Time.deltaTime;

            if (wanderCooldown <= 0f)
            {
                Vector3 randomDirection = Random.insideUnitSphere * wanderRadius;
                randomDirection += transform.position;
                NavMeshHit hit;

                if (NavMesh.SamplePosition(randomDirection, out hit, wanderRadius, NavMesh.AllAreas))
                {
                    agent.SetDestination(hit.position);
                }

                wanderCooldown = wanderTimer;
    }
}




    }

    public void ReceivePotion(string potionName)
    {
        if (potionName == requestedPotion)
        {
            Debug.Log("✅ Correct potion");
        }
        else
        {
          Debug.Log("❌ Wrong potion");
        }

        currentState = State.Idle;
        wanderCooldown = 0f; // triggers wandering immediately again
        manager.SendNextVillagerToBooth(); // next one approaches
    }


}
