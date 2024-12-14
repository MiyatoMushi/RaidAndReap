using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewQuest", menuName = "Quest System/Quest")]
public class Quest : ScriptableObject
{
    // General Quest Details
    [Header("Quest Details")]
    [SerializeField] private string questName;
    [SerializeField] private string description;
    [SerializeField] private bool isCompleted;

    [Header("Quest Type")]
    public QuestType Type;

    // Item Requirements
    [Header("Item Requirements")]
    [SerializeField] private bool requiresItem;
    [SerializeField] private string requiredItemName;
    [SerializeField] private int requiredItemCount;
    private int currentItemCount;

    // Kill Requirements
    [Header("Kill Requirements")]
    [SerializeField] private string[] requiredKillTargets;
    [SerializeField] private int[] requiredKillCounts;
    private int[] currentKillCounts;

    // Rewards
    [Header("Rewards")]
    [SerializeField] private int experienceReward;
    [SerializeField] private int goldReward;

    #region Properties
     public enum QuestType
    {
        Kill,
        Gather,
        Explore
    }
    public string QuestName { get => questName; set => questName = value; }
    public string Description { get => description; set => description = value; }
    public bool IsCompleted { get => isCompleted; set => isCompleted = value; }
    public bool RequiresItem { get => requiresItem; set => requiresItem = value; }
    public string RequiredItemName { get => requiredItemName; set => requiredItemName = value; }
    public int RequiredItemCount { get => requiredItemCount; set => requiredItemCount = value; }
    public int CurrentItemCount { get => currentItemCount; set => currentItemCount = value; }
    public string[] RequiredKillTargets { get => requiredKillTargets; set => requiredKillTargets = value; }
    public int[] RequiredKillCounts { get => requiredKillCounts; set => requiredKillCounts = value; }
    public int[] CurrentKillCounts { get => currentKillCounts; set => currentKillCounts = value; }
    public int ExperienceReward { get => experienceReward; set => experienceReward = value; }
    public int GoldReward { get => goldReward; set => goldReward = value; }
    #endregion

    #region Initialization
    public void InitializeKillCounts()
    {
        if (requiredKillTargets != null && requiredKillCounts != null && requiredKillTargets.Length == requiredKillCounts.Length)
        {
            currentKillCounts = new int[requiredKillTargets.Length];
        }
        else
        {
            Debug.LogError("Mismatch between requiredKillTargets and requiredKillCounts or one of them is null.");
            currentKillCounts = null;
        }
    }
    #endregion

    #region Item Handling
    public void AddItem(int count)
    {
        if (!requiresItem) return;

        currentItemCount += count;
        currentItemCount = Mathf.Clamp(currentItemCount, 0, requiredItemCount);
    }

    public bool HasRequiredItems()
    {
        return requiresItem && currentItemCount >= requiredItemCount;
    }
    #endregion

    #region Kill Handling
    public void AddKill(string target)
    {
        if (requiredKillTargets == null) return;

        for (int i = 0; i < requiredKillTargets.Length; i++)
        {
            if (requiredKillTargets[i] == target)
            {
                currentKillCounts[i]++;
                currentKillCounts[i] = Mathf.Clamp(currentKillCounts[i], 0, requiredKillCounts[i]);
                Debug.Log($"Kill progress updated for '{target}': {currentKillCounts[i]}/{requiredKillCounts[i]}");
            }
        }
    }

    public bool HasRequiredKills()
    {
        if (requiredKillTargets == null || requiredKillCounts == null) return false;

        for (int i = 0; i < requiredKillTargets.Length; i++)
        {
            if (currentKillCounts[i] < requiredKillCounts[i])
            {
                return false;
            }
        }
        return true;
    }
    #endregion

    #region Quest Completion
    public void CompleteQuest()
    {
        if (isCompleted)
        {
            Debug.LogWarning($"Quest '{questName}' is already completed.");
            return;
        }

        if ((requiresItem && !HasRequiredItems()) || (requiredKillTargets != null && !HasRequiredKills()))
        {
            Debug.LogWarning($"Quest '{questName}' cannot be completed. Requirements are not met.");
            return;
        }

        isCompleted = true;
        GrantRewards();
        Debug.Log($"Quest '{questName}' is completed!");
    }

    private void GrantRewards()
    {
        Debug.Log($"Granted {experienceReward} XP and {goldReward} gold.");
    }
    #endregion

    public void ResetProgress()
    {
        // Reset general quest completion status
        isCompleted = false;

        // Reset item progress
        currentItemCount = 0;

        // Reset kill progress
        if (requiredKillTargets != null && requiredKillCounts != null)
        {
            currentKillCounts = new int[requiredKillCounts.Length];
        }
        else
        {
            currentKillCounts = null;
        }

        Debug.Log($"Quest '{questName}' progress has been reset.");
    }
}

