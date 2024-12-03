using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public GameObject selectImage;

    public void Awake(){
        Deselect();
    }

    public void Select() {
        selectImage.SetActive(true);
    }

    public void Deselect() {
        selectImage.SetActive(false);
    }

    public void OnDrop(PointerEventData eventData) {
        if(transform.childCount ==0) {
            InventoryItem inventoryItem = eventData.pointerDrag.GetComponent<InventoryItem>();
            inventoryItem.parentAfterDrag = transform;
        }
    }
}
