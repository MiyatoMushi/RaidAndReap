using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestUI : MonoBehaviour
{
    public QuestManager questManager;
    public TMP_Text questLog;

    private void Update()
    {
        questLog.text = "";
        foreach (var quest in questManager.activeQuests)
        {
            questLog.text += $"{quest.questName}: {(quest.isCompleted ? "Completed" : "In Progress")}\n";
        }
    }
}
