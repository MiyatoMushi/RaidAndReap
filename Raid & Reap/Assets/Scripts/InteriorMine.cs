using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteriorMine : MonoBehaviour
{
    [SerializeField] private string sceneToLoad; // The target scene to load
    [SerializeField] private Vector3 playerSpawnPosition; // The spawn position for the player in the target scene

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collider belongs to the player
        if (collision.CompareTag("Player"))
        {
            // Register a callback to set the player's position once the scene is loaded
            SceneManager.sceneLoaded += OnSceneLoaded;

            // Load the target scene
            SceneManager.LoadScene(sceneToLoad);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Check if the scene loaded is the one we want
        if (scene.name == sceneToLoad)
        {
            // Find the player object in the newly loaded scene
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                // Set the player's position
                player.transform.position = playerSpawnPosition;
            }

            // Unregister the callback to avoid it being called again in the future
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }
}
