using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    private int itemMaxStack = 99;
    public InventorySlot[] inventorySlots;
    public GameObject inventoryItemPrefab;
    public AudioSource audioSource;
    public AudioClip selectSound;

    private int selectedSlot = -1;

    public Button actionButton;
    public Image actionImage; // The image inside the button that we want to change opacity of

    private Item currentTool;

    public void ChangeSelectedSlot(int newValue)
    {
        if (selectedSlot >= 0)
        {
            inventorySlots[selectedSlot].Deselect();
        }

        inventorySlots[newValue].Select();
        selectedSlot = newValue;

        // Get the selected item from the inventory slot
        InventoryItem selectedItem = inventorySlots[selectedSlot].GetComponentInChildren<InventoryItem>();

        if (selectedItem != null)
        {
            currentTool = selectedItem.item;

            // Check the item type and update the action button accordingly
            switch (selectedItem.item.itemType)
            {
                case Item.ItemType.Tool:
                    UpdateActionButtonOpacity(1f);
                    UpdateActionButtonSprite(selectedItem.item.itemIcon);
                    break;

                case Item.ItemType.Weapon:
                    // Example: Update button for Weapon (you can define the sprite for weapons)
                    UpdateActionButtonOpacity(1f);
                    UpdateActionButtonSprite(selectedItem.item.itemIcon);
                    break;

                case Item.ItemType.Consumable:
                    // Example: Update button for Consumable
                    UpdateActionButtonOpacity(1f);
                    UpdateActionButtonSprite(selectedItem.item.itemIcon);
                    break;

                case Item.ItemType.Decoration:
                    // Example: Update button for Decoration
                    UpdateActionButtonOpacity(1f);
                    UpdateActionButtonSprite(selectedItem.item.itemIcon);
                    break;

                default:
                    // If the item type doesn't match any of the above, hide the action button
                    currentTool = null;
                    UpdateActionButtonOpacity(0f);
                    ResetActionButtonSprite();
                    break;
            }
        }
        else
        {
            currentTool = null;
            UpdateActionButtonOpacity(0f);
            ResetActionButtonSprite();
        }

        if (audioSource != null && selectSound != null)
        {
            audioSource.PlayOneShot(selectSound);
        }
    }

    public void SpawnNewItem(Item item, InventorySlot slot)
    {
        GameObject newItemGo = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGo.GetComponent<InventoryItem>();

        inventoryItem.InitializeItem(item);
    }

    public bool AddItem(Item item)
    {
        if (item.stackable) // Check if the item is stackable
        {
            // Try to stack the item in existing slots
            for (int i = 0; i < inventorySlots.Length; i++)
            {
                InventorySlot slot = inventorySlots[i];
                InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

                // If the item is stackable, and the slot already has the same item, stack it
                if (itemInSlot != null && itemInSlot.item == item && itemInSlot.itemCount < itemMaxStack)
                {
                    itemInSlot.itemCount++;
                    itemInSlot.RefreshCount();
                    return true;
                }
            }   
        }

        // If item is not stackable or no space for stacking, spawn a new item
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

            if (itemInSlot == null) // If the slot is empty, spawn the item
            {
                SpawnNewItem(item, slot);
                return true;
            }
        }

        // Inventory is full, return false
        return false;
    }

    public void RemoveItem(Item item)
    {
        // Implementation of item removal (if needed)
    }

    //Update Buttons based sa item
    private void UpdateActionButtonOpacity(float alphaValue)
    {
        if (actionImage != null)
        {
            Color newColor = actionImage.color;
            newColor.a = alphaValue; // Set the alpha to the desired value
            actionImage.color = newColor;
        }
    }

    private void UpdateActionButtonSprite(Sprite newSprite)
    {
        if (actionImage != null)
        {
            actionImage.sprite = newSprite; // Set the sprite to the selected item's sprite
        }
    }

    // Reset the action button image sprite to a default or neutral sprite
    private void ResetActionButtonSprite()
    {
        if (actionImage != null)
        {
            // Set this to whatever sprite you'd like to be the default state of the action button (e.g., empty or neutral sprite)
            actionImage.sprite = null; // or assign a default sprite
        }
    }
}