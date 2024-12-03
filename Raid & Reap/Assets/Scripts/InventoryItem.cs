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

    public void InitializeItem(Item newItem) {
        item = newItem;
        image.sprite = newItem.itemIcon;
        //itemCount = Random.Range(1, 4);
        RefreshCount();
    }

    public void RefreshCount() {
        countText.text = itemCount.ToString();
        bool textActive = itemCount > 1;
        countText.gameObject.SetActive(textActive);
    }

    public void OnBeginDrag(PointerEventData eventData) {
        image.raycastTarget = false;
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        //throw new System.NotImplementedException();
    }

    public void OnDrag(PointerEventData eventData) {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData) {
        image.raycastTarget =true;
        transform.SetParent(parentAfterDrag);
        //throw new System.NotImplementedException();
    }
}
