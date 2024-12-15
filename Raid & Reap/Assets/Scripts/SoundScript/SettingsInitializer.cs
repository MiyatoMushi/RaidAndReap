using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsInitializer : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider SFXSlider;

    private void Start()
    {
        Volume_Controller volumeController = FindObjectOfType<Volume_Controller>();
        if (volumeController != null)
        {
       //     volumeController.InitializeSliders(musicSlider, SFXSlider);
        }
    }
}
