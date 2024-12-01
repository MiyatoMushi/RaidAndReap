using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu (fileName = "NewTool", menuName = "Inventory/Tool")]
public class Tool : ScriptableObject
{
    public string toolName;
    public int toolDamage;
    public int toolDurability;

    public void UseTool () {
        Debug.Log($"{toolName} attacks with {toolDamage} damage!");
    }
}
