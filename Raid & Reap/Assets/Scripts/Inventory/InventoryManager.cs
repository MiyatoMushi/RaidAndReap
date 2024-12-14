using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
    public InventoryData inventoryData; // Reference to the Scriptable Object

    private int itemMaxStack = 99;
    public InventorySlot[] inventorySlots;
    public GameObject inventoryItemPrefab;
    public AudioSource audioSource;
    public AudioClip selectSound;

    private int selectedSlot = -1;

    public Button actionButton;
    public Image actionImage; // The image inside the button that we want to change opacity of

    private Item currentTool;

    private void Awake () {
        instance = this;
    }

    void Start(){
        LoadInventory();
    }

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
                    AssignButtonFunction(() => UseTool(selectedItem.item));
                    break;

                case Item.ItemType.Weapon:
                    // Example: Update button for Weapon (you can define the sprite for weapons)
                    UpdateActionButtonOpacity(1f);
                    UpdateActionButtonSprite(selectedItem.item.itemIcon);
                    AssignButtonFunction(() => UseWeapon(selectedItem.item));
                    break;

                case Item.ItemType.Consumable:
                    // Example: Update button for Consumable
                    UpdateActionButtonOpacity(1f);
                    UpdateActionButtonSprite(selectedItem.item.itemIcon);
                    AssignButtonFunction(() => UseConsumable(selectedItem.item));
                    break;

                case Item.ItemType.Decoration:
                    // Example: Update button for Decoration
                    UpdateActionButtonOpacity(1f);
                    UpdateActionButtonSprite(selectedItem.item.itemIcon);
                    AssignButtonFunction(() => UseDecoration(selectedItem.item));
                    break;

                default:
                    // If the item type doesn't match any of the above, hide the action button
                    currentTool = null;
                    UpdateActionButtonOpacity(0f);
                    ResetActionButtonSprite();
                    AssignButtonFunction(null);
                    AssignButtonFunction(null);
                    break;
            }
        }
        else
        {
            currentTool = null;
            UpdateActionButtonOpacity(0f);
            ResetActionButtonSprite();
            AssignButtonFunction(null);
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
        AddItemToInventory(item);

        if (item.stackable) // Check if the item is stackable
        {
            // Try to stack the item in existing slots
            for (int i = 0; i < inventorySlots.Length; i++)
            {
                InventorySlot slot = inventorySlots[i];
                InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

                // If the item is stackable, and the slot already has the same item, stack it
                if (itemInSlot != null && itemInSlot.item.itemName == item.itemName && itemInSlot.itemCount < itemMaxStack)
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

    private void AssignButtonFunction(UnityEngine.Events.UnityAction action)
    {
        // Clear existing listeners to avoid stacking functions
        actionButton.onClick.RemoveAllListeners();

        if (action != null)
        {
            actionButton.onClick.AddListener(action);
        }
    }
    
    //Using Items;
    private void UseTool(Item tool)
    {
        LumberAxe lumberAxe =  FindObjectOfType<LumberAxe>();
        Debug.Log("Using Tool: " + tool.itemName);
        StartCoroutine(ButtonCooldown());
        DisableActionButton();
        // Add tool-specific logic here (e.g., chopping a tree)
        if (lumberAxe != null)
        {
            if (tool.itemName == "Rusty Lumber Axe")
            {
                lumberAxe.UseLumberAxe(currentTool.toolDamage, 1);
                Debug.Log("Swinging Rusty Lumber Axe!");
            }
            else if (tool.itemName == "Iron Lumber Axe")
            {
                lumberAxe.UseLumberAxe(currentTool.toolDamage, 2);
                Debug.Log("Swinging Iron Lumber Axe!");
            }
            else if (tool.itemName == "Gold Lumber Axe")
            {
                lumberAxe.UseLumberAxe(currentTool.toolDamage, 3);
                Debug.Log("Swinging Gold Lumber Axe!");
            }
        } 
        else
        {
            Debug.LogWarning("LumberAxe script not found in the scene!");
        }
    }

    private void UseWeapon(Item weapon)
    {
        Debug.Log("Using Weapon: " + weapon.itemName);

        // Add weapon-specific logic here (e.g., attacking an enemy)
        if (weapon.itemName.Contains("Rusty Sword") || weapon.itemName.Contains("Iron Sword") || weapon.itemName.Contains("Gold Sword"))
        {
            Debug.Log("Swinging Sword!");
            // Call your Sword-specific logic here
        }
    }

    private void UseConsumable(Item consumable)
    {
        Debug.Log("Consuming: " + consumable.itemName);

        // Add consumable-specific logic here (e.g., healing or buffs)
        if (consumable.itemName == "Health Potion")
        {
            Debug.Log("Drinking Health Potion!");
            // Call your potion logic here
        }
    }

    private void UseDecoration(Item decoration)
    {
        Debug.Log("Placing: " + decoration.itemName);
    }

    private IEnumerator ButtonCooldown() {
        yield return new WaitForSeconds(1f);
        EnableActionButton();
    }

    public void EnableActionButton() {
        actionButton.interactable = true;
    }

    public void DisableActionButton() {
        actionButton.interactable = false;
    }

/// Item Saving not finished

    public void AddItemToInventory(Item item)
    {
        if (item.stackable)
        {
            // Check if the item already exists in the inventory
            InventoryItemData existingItem = inventoryData.items.Find(i => i.itemName == item.itemName);

            if (existingItem != null)
            {
                // Increase the count if the item is stackable
                existingItem.itemCount++;
                return;
            }
        }

        // Add new item to the inventory (either because it is not stackable or doesn't exist)
        inventoryData.items.Add(new InventoryItemData
        {
            itemName = item.itemName,
            weaponDamage = item.weaponDamage,
            toolDamage = item.toolDamage,
            itemType = item.itemType.ToString(),
            actionType = item.actionType.ToString(),
            stackable = item.stackable,
            itemCount = 1,
            itemIcon = item.itemIcon
        });
    }

    public void RemoveItemFromInventory(Item item)
    {
        InventoryItemData existingItem = inventoryData.items.Find(i => i.itemName == item.itemName);

        if (existingItem != null)
        {
            existingItem.itemCount--;
            if (existingItem.itemCount <= 0)
            {
                inventoryData.items.Remove(existingItem);
            }
        }
    }

    public void LoadInventory()
    {
        // Populate slots from inventory data
        for (int i = 0; i < inventoryData.items.Count; i++)
        {
            if (i >= inventorySlots.Length) break; // Prevent exceeding available slots

            InventoryItemData itemData = inventoryData.items[i];

            // Check if the item can be stacked in existing slots
            bool itemStacked = TryStackItem(itemData);

            // If the item was not stacked, create a new one
            if (!itemStacked)
            {
                InventorySlot emptySlot = FindEmptySlot();
                if (emptySlot != null)
                {
                    SpawnItemInSlot(itemData, emptySlot);
                }
            }
        }
    }

    private void SpawnItemInSlot(InventoryItemData itemData, InventorySlot slot)
    {
        // Instantiate a new inventory item prefab
        GameObject newItemGo = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGo.GetComponent<InventoryItem>();

        // Initialize the item
        inventoryItem.InitializeItem(new Item
        {
            itemName = itemData.itemName,
            weaponDamage = itemData.weaponDamage,
            toolDamage = itemData.toolDamage,
            itemType = Enum.TryParse(itemData.itemType, out Item.ItemType parsedType) ? parsedType : Item.ItemType.Tool,
            actionType = Enum.TryParse(itemData.actionType, out Item.ActionType parsedAction) ? parsedAction : Item.ActionType.DamageEnemy,
            stackable = itemData.stackable,
            itemIcon = itemData.itemIcon
        });

        // Assign the correct count
        inventoryItem.itemCount = itemData.itemCount;
        inventoryItem.RefreshCount();
    }

    private bool TryStackItem(InventoryItemData itemData)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

            // Match items by name, ensure stacking only happens for stackable items
            if (itemInSlot != null 
                && itemInSlot.item.itemName == itemData.itemName 
                && itemInSlot.item.stackable 
                && itemInSlot.itemCount < itemMaxStack)
            {
                int availableSpace = itemMaxStack - itemInSlot.itemCount;

                // Add as many as possible to the existing stack
                int itemsToAdd = Mathf.Min(availableSpace, itemData.itemCount);
                itemInSlot.itemCount += itemsToAdd;
                itemInSlot.RefreshCount();

                // Reduce the count of the remaining items
                itemData.itemCount -= itemsToAdd;

                // If all items are stacked, return true
                if (itemData.itemCount <= 0)
                    return true;
            }
        }

        return false; // Could not stack all items
    }


    private InventorySlot FindEmptySlot()
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot == null) return slot; // Return the first empty slot
        }
        return null;
    }


}