using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;

    private Vector2 lastSpawnPoint;
    public float defaultX;
    public float defaultY;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes

            lastSpawnPoint = new Vector2(defaultX, defaultY); // Replace with your default coordinates
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetSpawnPoint(Vector2 spawnPoint)
    {
        lastSpawnPoint = spawnPoint;
    }

    public Vector2 GetSpawnPoint()
    {
        return lastSpawnPoint;
    }
}
