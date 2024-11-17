using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    //Scene Loader for Interior Environments - JC
    [SerializeField] private string sceneToLoad;

    private void OnTriggerEnter2D (Collider2D collision) {
        //Check Player Tag
        if (collision.CompareTag("Player")) {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
