using UnityEngine;
using TMPro;

public class QuestUI : MonoBehaviour
{
    public QuestManager questManager;
    public TMP_Text questLog;
    public TMP_Text requirement;

    private void Update()
    {
        questLog.text = "";
        requirement.text = "";

        // Track if all quests are completed
        bool allQuestsCompleted = true;

        foreach (var quest in questManager.ActiveQuests)
        {
            // Update quest log
            questLog.text += $"{quest.QuestName}: {(quest.IsCompleted ? "Completed" : "In Progress")}\n";

            switch (quest.Type)
            {
                case Quest.QuestType.Kill:
                    for (int i = 0; i < quest.RequiredKillCounts.Length; i++)
                    {
                        requirement.text += $"Kill {quest.RequiredKillTargets[i]}: {quest.CurrentKillCounts[i]} / {quest.RequiredKillCounts[i]}";
                    }
                    break;

                case Quest.QuestType.Gather:
                    requirement.text += $"Gather: {quest.CurrentItemCount} / {quest.RequiredItemCount}";
                    break;

                case Quest.QuestType.Explore:
                    requirement.text += "Explore the specified location.\n";
                    break;

                default:
                    Debug.LogWarning($"Unhandled quest type for '{quest.QuestName}'.");
                    break;
            }

            // If any quest is not completed, set allQuestsCompleted to false
            if (!quest.IsCompleted)
            {
                allQuestsCompleted = false;
            }
        }


        // Show "Quest Finished" message only if all quests are completed and there are quests
        if (questManager.ActiveQuests.Count > 0 && allQuestsCompleted)
        {
            QuestFinished();
        }
    }

    private void QuestFinished()
    {
        questLog.text = "All quests for today are finished!";
        requirement.text = "";
    }
}
