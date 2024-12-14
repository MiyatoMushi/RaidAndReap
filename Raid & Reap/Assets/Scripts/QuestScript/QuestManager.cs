using UnityEngine;
using System.Collections.Generic;

public class QuestManager : MonoBehaviour
{
    // Singleton Instance
    public static QuestManager Instance { get; private set; }

    // List of active quests
    private List<Quest> activeQuests = new List<Quest>();

    // Property to access the activeQuests list
    public List<Quest> ActiveQuests => new List<Quest>(activeQuests); // Return a copy for safety

    private void Awake()
    {
        // Implement Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optional: Persist across scenes
        }
        else
        {
            Debug.LogWarning("Duplicate QuestManager detected. Destroying new instance.");
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Adds a new quest to the active quests list.
    /// </summary>
    /// <param name="quest">The quest to add.</param>
    public void AddQuest(Quest quest)
    {
        if (quest == null)
        {
            Debug.LogWarning("Attempted to add a null quest.");
            return;
        }

        if (!activeQuests.Contains(quest))
        {
            quest.InitializeKillCounts();
            activeQuests.Add(quest);
            Debug.Log($"Quest added: {quest.QuestName}");

            // Notify other systems (e.g., UI updates)
            OnQuestAdded(quest);
        }
        else
        {
            Debug.LogWarning($"Quest '{quest.QuestName}' is already active.");
        }
    }

    /// <summary>
    /// Marks a quest as completed and removes it from the active list.
    /// </summary>
    /// <param name="quest">The quest to complete.</param>
    public void CompleteQuest(Quest quest)
    {
        if (quest == null) return;

        Debug.Log($"Quest '{quest.QuestName}' marked as complete.");
        quest.CompleteQuest();
    }

    /// <summary>
    /// Updates quests when a required item is added.
    /// </summary>
    /// <param name="item">The item that was added.</param>
    /// <param name="count">The number of items added.</param>
    public void OnItemAdded(string itemName, int count)
    {
        if (string.IsNullOrEmpty(itemName) || count <= 0)
        {
            Debug.LogWarning("Invalid item added. Item name cannot be null or empty, and count must be positive.");
            return;
        }

        // Temporary list to hold quests that need to be completed
        List<Quest> questsToComplete = new List<Quest>();

        foreach (var quest in activeQuests)
        {
            if (quest.RequiresItem && quest.RequiredItemName == itemName)
            {
                quest.AddItem(count);
                Debug.Log($"Quest '{quest.QuestName}' updated: {itemName} count is now {quest.CurrentItemCount}/{quest.RequiredItemCount}.");

                if (quest.HasRequiredItems() && !quest.IsCompleted)
                {
                    questsToComplete.Add(quest); // Mark the quest for completion
                }
            }
        }

        // Complete the quests after the iteration
        foreach (var quest in questsToComplete)
        {
            CompleteQuest(quest);
        }
    }

    // Optional: Events or hooks for other systems to listen for quest changes
    protected virtual void OnQuestAdded(Quest quest) { /* Notify UI or other systems */ }
    protected virtual void OnQuestCompleted(Quest quest) { /* Notify UI or other systems */ }
    protected virtual void OnObjectiveCompleted(Quest quest, string objective) { /* Notify UI or other systems */ }
    protected virtual void OnQuestItemUpdated(Quest quest, Item item) { /* Notify UI or other systems */ }

    /// <summary>
    /// Updates kill-based quests when a target is killed.
    /// </summary>
    /// <param name="target">The name of the killed target.</param>
    public void OnKillTarget(string target)
    {
        if (string.IsNullOrEmpty(target))
        {
            Debug.LogWarning("Killed target cannot be null or empty.");
            return;
        }

        // Temporary list to hold quests that need to be completed
        List<Quest> questsToComplete = new List<Quest>();

        foreach (var quest in activeQuests)
        {
            if (quest.RequiredKillTargets != null)
            {
                quest.AddKill(target);

                if (quest.HasRequiredKills() && !quest.IsCompleted)
                {
                    questsToComplete.Add(quest); // Mark the quest for completion
                }
            }
        }

        // Complete the quests after the iteration
        foreach (var quest in questsToComplete)
        {
            CompleteQuest(quest);
        }
    }

}
