using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class QuestGiver : MonoBehaviour
{
    [Header("Quest Settings")]
    public Quest quest; // The quest given by this NPC
    private QuestManager questManager; // Reference to the QuestManager

    [Header("Player Interaction")]
    private Transform player; // Reference to the player's transform
    public float detectionRange = 5f; // Range at which the player can interact

    [Header("UI Elements")]
    public GameObject interactionIcon; // Icon or button for interaction
    public Button interactionButton; // UI button to interact
    public Sprite defaultButtonIcon; // Default button sprite
    public Sprite interactButtonIcon; // Interaction button sprite

    private bool isPlayerInRange = false; // Tracks if the player is within range

    private void Start()
    {
        // Initialize the quest as not completed
        quest.ResetProgress();

        GameObject questManagers = GameObject.FindGameObjectWithTag("QuestManager");

        if (questManagers != null)
        {
            questManager = questManagers.GetComponent<QuestManager>();
            if (questManager == null)
            {
                Debug.LogError("GameObject with tag 'QuestManager' does not have a QuestManager component.");
            }
        }
        else
        {
            Debug.LogError("GameObject with tag 'QuestManager' not found in the scene.");
        }

        // Locate the player in the scene by tag
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player GameObject with tag 'Player' not found in the scene.");
        }

        // Ensure interaction UI is hidden initially
        if (interactionButton != null)
        {
            interactionButton.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (player == null) return;

        // Calculate distance between the player and this NPC
        float distance = Vector2.Distance(transform.position, player.position);

        // Check if the player is within detection range
        if (distance <= detectionRange)
        {
            if (!isPlayerInRange)
            {
                isPlayerInRange = true;
                HandlePlayerEnterRange();
            }
        }
        else
        {
            if (isPlayerInRange)
            {
                isPlayerInRange = false;
                HandlePlayerExitRange();
            }
        }
    }

    /// <summary>
    /// Handles actions when the player enters interaction range.
    /// </summary>
    private void HandlePlayerEnterRange()
    {
        ShowInteractionIcon();
        UpdateButtonIcon(interactButtonIcon);
        ChangeButtonFunction(HandleQuestInteraction);
    }

    /// <summary>
    /// Handles actions when the player exits interaction range.
    /// </summary>
    private void HandlePlayerExitRange()
    {
        HideInteractionIcon();
        UpdateButtonIcon(defaultButtonIcon);
        ChangeButtonFunction(DefaultButtonFunction);
    }

    /// <summary>
    /// Shows the interaction UI icon or button.
    /// </summary>
    private void ShowInteractionIcon()
    {
        if (interactionButton != null)
        {
            interactionButton.gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// Hides the interaction UI icon or button.
    /// </summary>
    private void HideInteractionIcon()
    {
        if (interactionButton != null)
        {
            interactionButton.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Handles the interaction with the quest giver.
    /// </summary>
    private void HandleQuestInteraction()
    {
        if (questManager == null || quest == null)
        {
            Debug.LogWarning("QuestManager or Quest reference is missing!");
            return;
        }

        if (quest.IsCompleted)
        {
            Debug.Log($"Quest '{quest.QuestName}' has been completed and rewards granted.");
        }
        else
        {
            // If the quest is not completed, add it to the player's active quests
            questManager.AddQuest(quest);
            Debug.Log($"Quest '{quest.QuestName}' has been added to the active quests.");
        }
    }

    /// <summary>
    /// Changes the function of the interaction button.
    /// </summary>
    /// <param name="newFunction">The new function to assign to the button.</param>
    private void ChangeButtonFunction(UnityAction newFunction)
    {
        if (interactionButton != null)
        {
            interactionButton.onClick.RemoveAllListeners(); // Clear previous listeners
            interactionButton.onClick.AddListener(newFunction);
        }
    }

    /// <summary>
    /// Updates the icon of the interaction button.
    /// </summary>
    /// <param name="icon">The new sprite for the button.</param>
    private void UpdateButtonIcon(Sprite icon)
    {
        if (interactionButton != null && interactionButton.GetComponent<Image>() != null)
        {
            interactionButton.GetComponent<Image>().sprite = icon;
        }
    }

    /// <summary>
    /// Default function for the interaction button when no specific action is needed.
    /// </summary>
    private void DefaultButtonFunction()
    {
        Debug.Log("Default button function executed.");
    }

    /// <summary>
    /// Draws a sphere in the editor to represent the detection range.
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
