using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableObject : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip hitSound;
    public int health = 5;
    public GameObject objectChild;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(int amount)
    {
        if (IsDestroyed()) return;

        health -= amount;
        Debug.Log("Object Health: " + health);
        audioSource.clip = hitSound;
        audioSource.Play();
    }

//Ayusin ko to mamaya
    public bool IsDestroyed()
    {
        if (health <= 0)
        {
            // Make the child invisible
            if (objectChild != null)
            {
                objectChild.SetActive(false);
                Debug.Log("Object Destroyed " + health);
            }

            return true; // Tree is destroyed
        }

        return false;
    }
}
