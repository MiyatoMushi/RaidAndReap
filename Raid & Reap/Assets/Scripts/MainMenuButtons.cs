using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    public AudioSource mainMenuButtonSound;
    public GameObject canvas;

public void NewButton()
    {
        mainMenuButtonSound.Play();
        SceneManager.LoadScene("PlayerFarm");
    }
    public void LoadButton() {
        mainMenuButtonSound.Play();
    }

    public void ExitButton() {
        mainMenuButtonSound.Play();
        Application.Quit();
        Debug.Log("Exit button clicked!");
    }

    public void AboutButton() {
        mainMenuButtonSound.Play();
        canvas.SetActive(false);
    }

    public void SettingsButton() {
        mainMenuButtonSound.Play();
        canvas.SetActive(false);
    }

    public void ReturnButton() {
        mainMenuButtonSound.Play();
        canvas.SetActive(false);
    }
    

    /* Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }*/
}
