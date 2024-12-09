using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewTool", menuName = "Inventory/Tool")]
public class Tool : ScriptableObject
{
    public Sprite toolIcon;
    public string toolName;
    public bool isStackable = false;
    public int toolDamage;
    public float toolRange;
}
