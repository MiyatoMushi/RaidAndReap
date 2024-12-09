using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeScript : MonoBehaviour, IDamageable
{
    public int health = 5; // Number of hits needed to destroy the tree

    public void TakeDamage(int amount)
    {
        health -= amount;
        Debug.Log("Tree took damage! Remaining health: " + health);
    }

    public bool IsDestroyed()
    {
        return health <= 0;
    }
}
