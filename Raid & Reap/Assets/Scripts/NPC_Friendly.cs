using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NPC_Friendly : MonoBehaviour
{
    public string[] messages;
    public float interactionRange = 2f; 
    private Transform player; 

    public GameObject interactionIcon;
    public TextMeshProUGUI messageText; 
    public Button interactionButton; 
    private bool isPlayerInRange = false;

    public float messageDuration = 3f;

    public GameObject messageBackground; 
    public float slideDuration = 1f; 

    private void Start()
    {

        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player GameObject with tag 'Player' not found in the scene.");
        }

        if (interactionButton != null)
        {
            interactionButton.gameObject.SetActive(false); 
            interactionButton.onClick.AddListener(InteractWithPlayer);
        }
        else
        {
            Debug.LogError("Interaction button not assigned in the Inspector.");
        }


        if (interactionIcon != null)
        {
            interactionIcon.SetActive(false);
        }

        if (messageText != null)
        {
            messageText.gameObject.SetActive(false);
        }

        if (messageBackground != null)
        {
            messageBackground.SetActive(false); 
        }
    }

    private void Update()
    {
        if (player == null) return;


        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer <= interactionRange)
        {
            if (!isPlayerInRange)
            {
                isPlayerInRange = true;
                ShowInteractionIcon();
                ShowInteractionButton(); 
            }
        }
        else
        {
            if (isPlayerInRange)
            {
                isPlayerInRange = false;
                HideInteractionIcon();
                HideInteractionButton();
                HideMessage();
            }
        }
    }

    private void ShowInteractionIcon()
    {
        if (interactionIcon != null)
        {
            interactionIcon.SetActive(true);
        }
    }

    private void HideInteractionIcon()
    {
        if (interactionIcon != null)
        {
            interactionIcon.SetActive(false);
        }
    }

    private void ShowInteractionButton()
    {
        if (interactionButton != null)
        {
            interactionButton.gameObject.SetActive(true); 
        }
    }

    private void HideInteractionButton()
    {
        if (interactionButton != null)
        {
            interactionButton.gameObject.SetActive(false);
        }
    }

    private void ShowMessage(string message)
    {
        if (messageText != null)
        {
            messageText.gameObject.SetActive(true);
            messageText.text = message; 
        }

        if (messageBackground != null)
        {
            messageBackground.SetActive(true);
        }

        StartCoroutine(SlideDownMessageAndBackground());
    }

    private void HideMessage()
    {
        if (messageText != null)
        {
            messageText.gameObject.SetActive(false);
        }

        if (messageBackground != null)
        {
            messageBackground.SetActive(false);
        }
    }

    private void InteractWithPlayer()
    {
        if (messages.Length > 0)
        {
            string randomMessage = messages[Random.Range(0, messages.Length)];
            Debug.Log("NPC says: " + randomMessage);


            ShowMessage(randomMessage);


            StartCoroutine(HideMessageAfterTime(messageDuration));
        }
        else
        {
            Debug.Log("NPC has nothing to say.");
        }
    }

    private IEnumerator SlideDownMessageAndBackground()
    {
        Vector3 initialPosition = new Vector3(messageText.transform.position.x, messageText.transform.position.y + 400f, messageText.transform.position.z); // Start above NPC
        Vector3 targetPosition = new Vector3(messageText.transform.position.x, messageText.transform.position.y, messageText.transform.position.z); // Final position (just above NPC)


        messageText.transform.position = initialPosition;
        messageBackground.transform.position = initialPosition;

        float elapsedTime = 0f;
        while (elapsedTime < slideDuration)
        {
            messageText.transform.position = Vector3.Lerp(initialPosition, targetPosition, (elapsedTime / slideDuration));
            messageBackground.transform.position = Vector3.Lerp(initialPosition, targetPosition, (elapsedTime / slideDuration));

            elapsedTime += Time.deltaTime;
            yield return null;
        }


        messageText.transform.position = targetPosition;
        messageBackground.transform.position = targetPosition;
    }


    private IEnumerator HideMessageAfterTime(float duration)
    {
        yield return new WaitForSeconds(duration);
        HideMessage(); 
    }

    private void OnDrawGizmosSelected()
    {
        // Visual guide para sa range 
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, interactionRange);
    }
}
