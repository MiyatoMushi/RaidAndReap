using UnityEngine;

[CreateAssetMenu(fileName = "NewQuest", menuName = "Quest System/Quest")]
public class Quest : ScriptableObject
{
    public string questName;
    public string description;
    public bool isCompleted;

    [Header("Objectives")]
    public string[] objectives;

    [Header("Rewards")]
    public int experienceReward;
    public int goldReward;

    public void CompleteQuest()
    {
        isCompleted = true;
        Debug.Log($"{questName} is completed!");
        // Add logic for giving rewards here
    }
}
