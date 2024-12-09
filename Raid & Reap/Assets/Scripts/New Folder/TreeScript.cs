using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeScript : MonoBehaviour, IDamageable
{
    public int health = 5;
    public GameObject treeChild;
    private SpriteRenderer treeSpriteRenderer;

    void Start()
    {
        treeSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(int amount)
    {
        IsDestroyed();
        health -= amount;
        Debug.Log("Tree took damage! Remaining health: " + health);
    }

    public bool IsDestroyed()
    {
        if (health <= 1)
        {
            // Make the child invisible
            if (treeChild != null)
            {
                treeChild.SetActive(false);
                Debug.Log("You Cut Down A Tree: " + health);
            }

            return true; // Tree is destroyed
        }

        return false;
    }
}
