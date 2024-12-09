using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    [Header("Gameplay")]
    public string itemName;
    public int weaponDamage;
    public int toolDamage;
    public ItemType itemType;
    public ActionType actionType;

    [Header("UI")]
    public bool stackable = true;

    [Header("Both")]
    public Sprite itemIcon;

    public enum ItemType {
        Seed,
        Decoration,
        Tool,
        Weapon,
        Consumable
    }

    public enum ActionType {
        DamageEnemy,
        DestroyRock,
        DestroyWood,
        CutPlant,
        TillSoil
    }
}
