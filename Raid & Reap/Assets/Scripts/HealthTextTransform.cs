using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthTextTransform : MonoBehaviour
{
    // Reference to the health bar fill image
    public Image fillImage; 
    
    // Reference to the player's health text (foreground)
    public TMP_Text healthText; 
    
    // Reference to the player's health text (background shadow)
    public TMP_Text bgHealthText; 
    
    // Reference to the player GameObject
    private GameObject player; 

    void Start()
    {
        // Find and assign the player GameObject by its tag
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        // Update the health bar's fill amount based on the player's health (percentage)
        fillImage.fillAmount = PlayerStats.PlayerHealth * 0.01f;
        
        // Update both health text elements to display the current health
        healthText.text = PlayerStats.PlayerHealth + "/100";
        bgHealthText.text = PlayerStats.PlayerHealth + "/100";

        // Change the health bar color to red if health is 30 or below
        if (PlayerStats.PlayerHealth <= 30)
        {
            fillImage.color = Color.red;
        }
        else
        {
            fillImage.color = Color.green; // Reset color to green for higher health
        }

        // Handle the case where the player's health reaches 0
        if (PlayerStats.PlayerHealth <= 0)
        {
            // Reset health bar color to green for a fresh start
            fillImage.color = Color.green;
            
            // Respawn the player at the starting position
            player.transform.position = PlayerStats.playerStartingPosition;
            
            // Reset the player's stats
            PlayerStats.PlayerResets();
        }
    }
}
