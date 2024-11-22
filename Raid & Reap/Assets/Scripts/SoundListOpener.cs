using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundListOpener : MonoBehaviour
{
    public GameObject soundList;

    public void soundListOpener(){
        soundList.SetActive(true);
    }

    public void SoundListClose(){
        soundList.SetActive(false);
    }
}
