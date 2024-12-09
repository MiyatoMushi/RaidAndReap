using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseOpenQuestUI : MonoBehaviour
{
    public GameObject closedQuestUI;
    public GameObject openQuestUI;

    void Start(){
        openQuestUI.SetActive(true);
        closedQuestUI.SetActive(false);
    }

    public void CloseQuest(){
        openQuestUI.SetActive(false);
        closedQuestUI.SetActive(true);
    }

    public void OpenQuest(){
        openQuestUI.SetActive(true);
        closedQuestUI.SetActive(false);
    }
}
