using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    public int health = 6; 
    public Sprite halfHealthSprite; 
    public GameObject logPrefab; 
    public GameObject leaves;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health == 3 && halfHealthSprite != null)
        {
            spriteRenderer.sprite = halfHealthSprite;
            Destroy(leaves);
        }

        if (health <= 0)
        {
            SpawnLog(); 
            Destroy(gameObject); 
        }
    }

    void SpawnLog()
    {
        if (logPrefab != null)
        {
            Vector3 spawnPosition = transform.position + new Vector3(0f, 0f, 0f); 
            Instantiate(logPrefab, spawnPosition, Quaternion.identity); 
        }
    }
}