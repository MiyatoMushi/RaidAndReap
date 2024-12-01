using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int playerInventorySize = 20;
    public List<ItemSlot> slots = new List<ItemSlot>();
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < playerInventorySize; i++) {
            slots.Add(new ItemSlot());
        }
    }
    [System.Serializable]
    public class ItemSlot {
        //public Item item;
        public int quantity;
        
        //public bool IsEmpty() => item == null;
    }
}
