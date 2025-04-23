using UnityEngine;

public class InteractGlider : MonoBehaviour
{
    public GliderController gliderController;
    private bool playerInRange = false;
    private GameObject playerRef;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("🛫 Mounting glider");

            playerRef.SetActive(false);
            gliderController.Activate();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            playerRef = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            playerRef = null;
        }
    }
}
