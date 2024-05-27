using UnityEngine;

public class PickupItem : MonoBehaviour
{
    public bool canPickup = false; // Set this to true when the player picks up an item
    private Item itemToPickup; // The item to be picked up

    private Inventory inventory; // Reference to the inventory script

    void Start()
    {
        inventory = FindObjectOfType<Inventory>(); // Find the inventory in the scene
    }

    void Update()
    {
        if (canPickup)
        {
            PickUp();
            canPickup = false; // Reset the pickup flag
        }
    }

    public void SetItemToPickup(Item item)
    {
        itemToPickup = item; // Set the item to be picked up
    }

    void PickUp()
    {
        if (inventory != null && itemToPickup != null)
        {
            inventory.AddItem(itemToPickup); // Add the item to the inventory
            Debug.Log("Picked up item: " + itemToPickup.itemName);
        }
    }
}
