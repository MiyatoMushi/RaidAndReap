using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IPointerClickHandler, IDropHandler
{
    public Sprite defaultSprite; // Default background sprite for the slot
    public Sprite selectedSprite; // Sprite to show when the slot is selected
    public Image slotImage; // Reference to the Image component

    private InventoryManager inventoryManager; // Reference to the inventory manager

    private void Awake()
    {
        inventoryManager = FindObjectOfType<InventoryManager>();
        Deselect();
    }

    public void Select()
    {
        slotImage.sprite = selectedSprite;
    }

    public void Deselect()
    {
        slotImage.sprite = defaultSprite;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (HasItem())
        {
            inventoryManager.ChangeSelectedSlot(GetSlotIndex());
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        InventoryItem inventoryItem = eventData.pointerDrag.GetComponent<InventoryItem>();
        if (inventoryItem != null)
        {
            if (transform.childCount == 0) // Only allow dropping if the slot is empty
            {
                Transform previousParent = inventoryItem.parentAfterDrag;
                inventoryItem.parentAfterDrag = transform;

                // Deselect the previous slot
                InventorySlot previousSlot = previousParent.GetComponent<InventorySlot>();
                if (previousSlot != null)
                {
                    previousSlot.Deselect();
                }

                // Select the new slot
                Select();
                inventoryManager.ChangeSelectedSlot(GetSlotIndex());
            }
        }
    }

    private bool HasItem()
    {
        return transform.childCount > 0;
    }

    private int GetSlotIndex()
    {
        for (int i = 0; i < inventoryManager.inventorySlots.Length; i++)
        {
            if (inventoryManager.inventorySlots[i] == this)
            {
                return i;
            }
        }
        return -1;
    }
}