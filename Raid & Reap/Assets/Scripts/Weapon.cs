using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName= "Weapon/", menuName = "Inventory/Weapon")]
public class Weapon : ScriptableObject
{
    public string weaponName;
    public int weaponDamage;

    public void UseWeapon() {
        Debug.Log($"{name} attacks with {weaponDamage} damage!");
    }
}
