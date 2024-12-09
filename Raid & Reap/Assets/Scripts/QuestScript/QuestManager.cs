using UnityEngine;
using System.Collections.Generic;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance { get; private set; }
    public List<Quest> activeQuests;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Prevent the object from being destroyed on scene change
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }

    public void AddQuest(Quest quest)
    {
        if (!activeQuests.Contains(quest))
        {
            activeQuests.Add(quest);
            Debug.Log($"Quest added: {quest.questName}");
        }
    }

    public void CompleteQuest(Quest quest)
    {
        if (activeQuests.Contains(quest))
        {
            quest.CompleteQuest();
            activeQuests.Remove(quest);
        }
    }

    public void CheckQuestProgress(string objective)
    {
        foreach (var quest in activeQuests)
        {
            foreach (var obj in quest.objectives)
            {
                if (obj == objective)
                {
                    Debug.Log($"Objective '{objective}' completed for quest '{quest.questName}'!");
                    // Mark the objective as completed and check if the quest is done
                    quest.isCompleted = true;
                }
            }
        }
    }
}
