using UnityEngine;

public class PlayerGrab : MonoBehaviour
{
    public Transform holdPoint;
    private GameObject heldObject;

    private GameObject hoveredObject;
    private Outline currentOutline;

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 3f))
        {
            GameObject hitObj = hit.collider.gameObject;

            // Outline logic
            if (hoveredObject != hitObj)
            {
                if (currentOutline != null) currentOutline.enabled = false;

                hoveredObject = hitObj;
                currentOutline = hoveredObject.GetComponent<Outline>();

                if (currentOutline != null) currentOutline.enabled = true;
            }
        }
        else
        {
            if (currentOutline != null) currentOutline.enabled = false;
            hoveredObject = null;
            currentOutline = null;
        }

        // Pickup/drop logic
        if (Input.GetMouseButtonDown(0)) // Left-click
        {
            if (heldObject == null)
            {
                TryPickup();
                Debug.Log("Attempting to pick up an object");
            }
            else
            {
                Drop();
            }
        }
    }

    void TryPickup()
    {
        Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0);
        Ray ray = Camera.main.ScreenPointToRay(screenCenter);

        if (Physics.Raycast(ray, out RaycastHit hit, 3f))
        {
            Debug.Log("Raycast hit: " + hit.collider.name);

            if (hit.collider.TryGetComponent(out Ingredient ingredient))
            {
                Debug.Log("Ingredient found: " + ingredient.name);
                heldObject = hit.collider.gameObject;
                heldObject.GetComponent<Rigidbody>().isKinematic = true;
                heldObject.transform.SetParent(holdPoint);
                heldObject.transform.localPosition = Vector3.zero;
                Debug.Log("Picked up " + heldObject.name);
            }
            else
            {
                Debug.LogWarning("Hit object does not have an Ingredient component.");
            }
        }
        else
        {
            Debug.LogWarning("Raycast did not hit anything.");
        }
    }

    void Drop()
    {
        heldObject.transform.SetParent(null);
        heldObject.GetComponent<Rigidbody>().isKinematic = false;
        Debug.Log("Dropped " + heldObject.name);
        heldObject = null;
    }
}
