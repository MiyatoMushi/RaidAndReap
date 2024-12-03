using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitBox : MonoBehaviour
{
    public SpriteRenderer playerHitbox;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Boar"))
        {
            // Example: Deal damage to the enemy
            playerHitbox.color = new Color(1f, 0f, 0f, 1f);
            PlayerStats.boarHealth -= 1;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        playerHitbox.color = new Color(1f, 0f, 0f, 0f);
    }
}
