using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InventoryData", menuName = "Inventory/InventoryData")]
public class InventoryData : ScriptableObject
{
    public List<InventoryItemData> items = new List<InventoryItemData>();
}

[System.Serializable]
public class InventoryItemData
{
    public string itemName;
    public int weaponDamage;
    public int toolDamage;
    public string itemType;
    public string actionType;
    public bool stackable;
    public int itemCount;
    public Sprite itemIcon; // Optional: Store the item's icon
}
