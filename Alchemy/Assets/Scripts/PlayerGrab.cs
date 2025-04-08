using UnityEngine;

public class PlayerGrab : MonoBehaviour
{
    public Transform holdPoint;
    private GameObject heldObject;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left-click
        {
            if (heldObject == null)
            {
                TryPickup();
            }
            else
            {
                Drop();
            }
        }
    }

    void TryPickup()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 3f)) //Range to grab the item
        {
            if (hit.collider.TryGetComponent(out Ingredient ingredient))
            {
                heldObject = hit.collider.gameObject;
                heldObject.GetComponent<Rigidbody>().isKinematic = true;
                heldObject.transform.SetParent(holdPoint);
                heldObject.transform.localPosition = Vector3.zero;
            }
        }
    }

    void Drop()
    {
        heldObject.transform.SetParent(null);
        heldObject.GetComponent<Rigidbody>().isKinematic = false;
        heldObject = null;
    }
}
