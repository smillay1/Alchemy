using UnityEngine;

public class GliderController : MonoBehaviour
{
    public Camera characterCamera;
    public Rigidbody rootRigidbody;
    public float moveSpeed = 20f;
    public float turnSpeed = 2f;
    public bool activated = false;

    public GameObject playerPrefab;
    public Transform dismountPoint;

    void Start()
    {
        rootRigidbody = GetComponent<Rigidbody>();
        if (activated)
            Activate();
    }

    void Update()
    {
        if (!activated) return;

        HandleInput();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Deactivate();
        }
    }

    void HandleInput()
    {
        // Forward movement
        rootRigidbody.linearVelocity = transform.forward * moveSpeed;

        float pitch = 0f;
        float roll = 0f;

        if (Input.GetKey(KeyCode.W)) pitch = 1f;
        if (Input.GetKey(KeyCode.S)) pitch = -1f;
        if (Input.GetKey(KeyCode.A)) roll = -1f;
        if (Input.GetKey(KeyCode.D)) roll = 1f;

        float yaw = Input.GetAxis("Mouse X") * turnSpeed;

        transform.Rotate(pitch * turnSpeed * Time.deltaTime, yaw * turnSpeed * Time.deltaTime, -roll * turnSpeed * Time.deltaTime, Space.Self);
    }

    public void Activate()
    {
        activated = true;
        characterCamera.enabled = true;
        characterCamera.GetComponent<AudioListener>().enabled = true;
    }

    public void Deactivate()
    {
        activated = false;
        characterCamera.enabled = false;
        characterCamera.GetComponent<AudioListener>().enabled = false;
        characterCamera.tag = "Untagged";

        GameObject player = Instantiate(playerPrefab, dismountPoint.position, dismountPoint.rotation);
        Debug.Log("👤 Player instantiated");

        Camera playerCam = player.GetComponentInChildren<Camera>();
        if (playerCam != null)
        {
            playerCam.enabled = true;
            playerCam.GetComponent<AudioListener>().enabled = true;
            playerCam.tag = "MainCamera";
            Debug.Log("✅ Player camera switched back");
        }
        else
        {
            Debug.LogError("❌ No Camera found on player prefab");
        }
    }
}
