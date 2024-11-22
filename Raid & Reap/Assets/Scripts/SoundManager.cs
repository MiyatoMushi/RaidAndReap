using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public AudioSource audioPlayer;
    public AudioClip clip;
    public TMP_Text playText;
    public Image playButton;

    void Update(){
        if (!audioPlayer.isPlaying){
            playButton.color = Color.green;
        playText.text = "Play";
        }
    }
    
    public void playAudio(){
        audioPlayer.clip = clip;
        playButton.color = Color.red;
        playText.text = "Playing...";
        audioPlayer.Play();
    }

    
}
