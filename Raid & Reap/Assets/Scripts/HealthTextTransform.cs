using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HealthTextTransform : MonoBehaviour
{
    public Image fillImage; // Green health bar image
    public TMP_Text healthText; // Player Health
    public TMP_Text bgHealthText; // Player Health
    private GameObject player;

    void Start(){
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        fillImage.fillAmount = PlayerStats.PlayerHealth * 0.01f;
        healthText.text = PlayerStats.PlayerHealth + "/100";
        bgHealthText.text = PlayerStats.PlayerHealth + "/100";
        if (PlayerStats.PlayerHealth <= 30){
            fillImage.color = Color.red;
        }

        if (PlayerStats.PlayerHealth <= 0){
            fillImage.color = Color.green;
            player.transform.position = PlayerStats.playerStartingPosition;
            PlayerStats.PlayerResets();
        }
    }
}
