using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    private void Start()
    {
        // Check if a spawn manager exists
        if (SpawnManager.instance != null)
        {
            // Set the player's position to the last recorded spawn point
            transform.position = SpawnManager.instance.GetSpawnPoint();
        }
    }
}
