using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("UI")]
    public Image image;
    public Text countText;

    [HideInInspector] public Transform parentAfterDrag;
    [HideInInspector] public int itemCount = 1;
    [HideInInspector] public Item item;

    public void InitializeItem(Item newItem)
    {
        item = newItem;
        image.sprite = newItem.itemIcon;
        RefreshCount();
    }

    public void RefreshCount()
    {
        countText.text = itemCount.ToString();
        countText.gameObject.SetActive(itemCount > 1);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        image.raycastTarget = false;
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root); // Temporarily detach from the slot
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        image.raycastTarget = true;

        // Snap back to the previous parent if not dropped in a valid slot
        if (transform.parent == transform.root)
        {
            transform.SetParent(parentAfterDrag);
        }
    }

    private void UseTool(Item tool)
    {
        Debug.Log("Using Tool: " + tool.itemName);

        // Add the logic for using a tool (e.g., chopping a tree)
        if (tool.itemName == "LumberAxe")
        {
            Debug.Log("Swinging LumberAxe!");
            // Call your LumberAxe swing logic here
        }
    }
}