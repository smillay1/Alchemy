using UnityEngine;

public class DeliveryZone : MonoBehaviour
{
    public VillagerManager manager;

    private void OnTriggerEnter(Collider other)
        {
            Ingredient potion = other.GetComponent<Ingredient>();
            if (potion != null)
            {
                VillagerAI villager = manager.GetCurrentVillager();
                if (villager != null && villager.currentState == VillagerAI.State.Waiting)
                {
                    villager.ReceivePotion(potion.ingredientName);
                    Destroy(other.gameObject); // remove potion
                }
            }
        }

}
