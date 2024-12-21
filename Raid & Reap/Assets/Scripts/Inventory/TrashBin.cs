using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class TrashBin : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        InventoryItem inventoryItem = eventData.pointerDrag.GetComponent<InventoryItem>();

        if (inventoryItem != null)
        {
            // Remove the item from the inventory
            InventoryManager.instance.RemoveItem(inventoryItem.item);

            // Destroy the GameObject of the dragged item
            Destroy(inventoryItem.gameObject);

            Debug.Log("Item trashed: " + inventoryItem.item.itemName);
        }
    }
}
