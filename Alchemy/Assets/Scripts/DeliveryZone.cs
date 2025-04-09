using UnityEngine;

public class DeliveryZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Ingredient potion = other.GetComponent<Ingredient>();
        if (potion != null)
        {
            VillagerAI villager = FindObjectOfType<VillagerAI>(); // single-villager version
            if (villager != null && villager.currentState == VillagerAI.State.Waiting)
            {
                villager.ReceivePotion(potion.ingredientName);
                Destroy(other.gameObject); // remove potion from scene
            }
        }
    }
}
