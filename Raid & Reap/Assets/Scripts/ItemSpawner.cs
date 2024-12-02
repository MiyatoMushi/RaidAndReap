using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public Item[] itemsToSpawn;

    public void SpawnTheItem(int id) {
        bool result = inventoryManager.AddItem(itemsToSpawn[id]);
        if(result == true) {
            Debug.Log("Item Added");
        } else {
            Debug.Log("Inventory is Full");
        }
    }
}
