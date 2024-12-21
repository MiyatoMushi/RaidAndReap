using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] private string sceneToLoad; // The target scene to load
    [SerializeField] private Transform spawnPoint; // The spawn point for the next scene

    private static Vector3 playerSpawnPosition; // Shared spawn position for the player across scenes

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collider belongs to the player
        if (collision.CompareTag("Player"))
        {
            // Save the spawn point position
            if (spawnPoint != null)
            {
                playerSpawnPosition = spawnPoint.position;
            }
            else
            {
                Debug.LogWarning("SpawnPoint is not set for this SceneChanger!");
            }

            // Load the target scene
            SceneManager.LoadScene(sceneToLoad);
        }
    }

    private void Start()
    {
        // If a valid spawn position is set, move the player there
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null && playerSpawnPosition != Vector3.zero)
        {
            player.transform.position = playerSpawnPosition;
        }
    }
}
