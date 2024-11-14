using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButtons : MonoBehaviour
{
    public AudioSource mainMenuButtonSound;

    public void NewButton() {
        mainMenuButtonSound.Play();
    }

    public void LoadButton() {
        mainMenuButtonSound.Play();
    }

    public void ExitButton() {
        mainMenuButtonSound.Play();
    }

    public void AboutButton() {
        mainMenuButtonSound.Play();
    }

    public void SettingsButton() {
        mainMenuButtonSound.Play();
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
