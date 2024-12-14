using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropPickup : MonoBehaviour
{
    [Header("Pickup Settings")]
    public float pickupRange = 2f;   // Range within which the player can pick up the item
    public float moveSpeed = 5f;    // Speed at which the item moves to the player

    [Header("Item Information")]
    public Item itemDrop;           // ScriptableObject for the item
    public int itemQuantity;        // Quantity of the item to add

    private Transform player;       // Reference to the player
    private InventoryManager inventoryManagerScript; // Reference to the InventoryManager script
    private QuestManager questManager; // Reference to the QuestManager

    void Start()
    {
        // Find the player by tag
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player not found! Ensure the player GameObject has the 'Player' tag.");
        }

        GameObject questManagers = GameObject.FindGameObjectWithTag("QuestManager");

        if (questManagers != null)
        {
            questManager = questManagers.GetComponent<QuestManager>();
            if (questManager == null)
            {
                Debug.LogError("GameObject with tag 'QuestManager' does not have a QuestManager component.");
            }
        }
        else
        {
            Debug.LogError("GameObject with tag 'QuestManager' not found in the scene.");
        }

        // Find the InventoryManager by tag
        GameObject inventoryManager = GameObject.FindGameObjectWithTag("InventoryManager");
        if (inventoryManager != null)
        {
            inventoryManagerScript = inventoryManager.GetComponent<InventoryManager>();
        }
        else
        {
            Debug.LogError("InventoryManager not found! Ensure there is a GameObject tagged 'InventoryManager' in the scene.");
        }
    }

    void Update()
    {
        // Skip processing if the player reference is missing
        if (player == null) return;

        // Check if the player is within range
        float distance = Vector2.Distance(transform.position, player.position);
        if (distance <= pickupRange)
        {
            // Move the item toward the player
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);

            // Check if the item has reached the player
            if (distance <= 0.1f) // Adjust threshold as needed
            {
                Pickup();
            }
        }
    }

    void Pickup()
    {
        // Add the item to the inventory
        if (inventoryManagerScript != null)
        {
            for (int i = 0; i < itemQuantity; i++)
            {
                bool result = inventoryManagerScript.AddItem(itemDrop);
                Debug.Log($"Picked up {itemQuantity} of {itemDrop.name}");

                if(result == true) {
                    Debug.Log("Item Added");
                } else {
                    Debug.Log("Inventory is Full");
                }
            }
        }
        else
        {
            Debug.LogError("InventoryManager script reference is missing! Cannot add item to inventory.");
        }

        questManager.OnItemAdded(itemDrop.name, itemQuantity);
        // Destroy the item GameObject
        Destroy(gameObject);
    }
}
