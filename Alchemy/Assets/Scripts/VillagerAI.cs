using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class VillagerAI : MonoBehaviour
{
    public enum State { Idle, Walking, Waiting, Leaving }

    public State currentState = State.Walking;

    // Where the booth is
    public Transform targetTransform;

    // The potion the villager is requesting
    public string requestedPotion;

    // Where the villager goes after getting the potion
    public Transform exitPoint;

    public NavMeshAgent agent;
    public VillagerManager manager;

    private float wanderRadius = 5f;
    private float wanderTimer = 5f;
    private float wanderCooldown;
    public bool taskCompleted = false;

    public GameObject requestUIPrefab;
    public Transform uiSpawnPoint;
    private GameObject requestUIInstance;
    private TextMeshProUGUI requestUIText;

    void Start()
    {
        manager.RegisterVillager(this);

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;

        if (requestUIPrefab != null && uiSpawnPoint != null)
        {
            requestUIInstance = Instantiate(requestUIPrefab, uiSpawnPoint.position, Quaternion.identity, uiSpawnPoint);
            requestUIText = requestUIInstance.GetComponentInChildren<TextMeshProUGUI>();
            UpdatePotionRequest();
        }

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
                if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, wanderRadius, NavMesh.AllAreas))
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
            TrustManager.Instance.ModifyTrust(+1);
        }
        else
        {
            Debug.Log("❌ Wrong potion");
            TrustManager.Instance.ModifyTrust(-1);
        }

        taskCompleted = true;
        currentState = State.Idle;
        wanderCooldown = 0f; // triggers wandering immediately again
        manager.SendNextVillagerToBooth(); // next one approaches
        manager.CheckIfAllTasksComplete();
    }

    private void UpdatePotionRequest()
    {
        if (requestUIText != null)
        {
            requestUIText.text = requestedPotion;
        }
    }
}
