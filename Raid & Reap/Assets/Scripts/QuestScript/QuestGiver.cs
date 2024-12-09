using UnityEngine;
using UnityEngine.UI;

public class QuestGiver : MonoBehaviour
{
    public Quest quest;
    public QuestManager questManager;

    private Transform player; // Reference to the player's transform
    public float detectionRange = 5f; // Range of detection

    public Button interactionButton;
    public Sprite defaultButtonIcon;
    public Sprite interactButtonIcon;

    private bool isPlayerInRange = false;

    void Start(){
        quest.isCompleted = false;
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player GameObject with tag 'Player' not found in the scene.");
        }
    }

    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.position);
        if (distance <= detectionRange)
        {
            if (!isPlayerInRange)
            {
                isPlayerInRange = true;
                UpdateButtonIcon(interactButtonIcon);
                ChangeButtonFunction(questGiver);
            }
        }
        else
        {
            if (isPlayerInRange)
            {
                isPlayerInRange = false;
                UpdateButtonIcon(defaultButtonIcon);
                ChangeButtonFunction(DefaultButtonFunction);
            }
        }
    }

    public void questGiver()
    {
        if (quest.isCompleted == true){
            questManager.CompleteQuest(quest);
        }
        else {
            questManager.AddQuest(quest);
        }
    }

    private void ChangeButtonFunction(UnityEngine.Events.UnityAction newFunction)
    {
        if (interactionButton != null)
        {
            interactionButton.onClick.RemoveAllListeners();
            interactionButton.onClick.AddListener(newFunction);
        }
    }

    private void UpdateButtonIcon(Sprite icon)
    {
        if (interactionButton != null && interactionButton.GetComponent<Image>() != null)
        {
            interactionButton.GetComponent<Image>().sprite = icon;
        }
    }

    private void DefaultButtonFunction()
    {
        Debug.Log("Default button function executed.");
    }

    void OnDrawGizmos()
    {
        // Draw a sphere to represent the detection range
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
