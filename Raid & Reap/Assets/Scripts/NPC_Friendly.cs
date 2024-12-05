using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NPC_Friendly : MonoBehaviour
{
    public string[] messages;
    public float interactionRange = 0.5f;
    private Transform player;

    public GameObject interactionIcon;
    public TextMeshProUGUI messageText;
    public Button interactionButton;
    private bool isPlayerInRange = false;

    public GameObject messageBackground;
    public float slideDuration;
    public float leaveCountdown; 

    public Sprite defaultButtonIcon;
    public Sprite interactButtonIcon;

    private Coroutine slideCoroutine;
    private Coroutine countdownCoroutine;
    private bool isMessageDisplayed = false;

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
            UpdateButtonIcon(defaultButtonIcon);
            interactionButton.onClick.AddListener(DefaultButtonFunction);
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
                UpdateButtonIcon(interactButtonIcon);
                ChangeButtonFunction(InteractWithPlayer);

                if (countdownCoroutine != null)
                {
                    StopCoroutine(countdownCoroutine);
                    countdownCoroutine = null;
                }
            }
        }
        else
        {
            if (isPlayerInRange)
            {
                isPlayerInRange = false;
                HideInteractionIcon();
                UpdateButtonIcon(defaultButtonIcon);
                ChangeButtonFunction(DefaultButtonFunction);

                if (isMessageDisplayed && countdownCoroutine == null)
                {
                    countdownCoroutine = StartCoroutine(CloseMessageAfterCountdown());
                }
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

    private void UpdateButtonIcon(Sprite icon)
    {
        if (interactionButton != null && interactionButton.GetComponent<Image>() != null)
        {
            interactionButton.GetComponent<Image>().sprite = icon;
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

        if (interactionButton != null)
        {
            interactionButton.interactable = false; 
        }

        if (slideCoroutine != null)
        {
            StopCoroutine(slideCoroutine); 
        }

        slideCoroutine = StartCoroutine(SlideDownMessageAndBackground());
    }

    private void HideMessage()
    {
        if (slideCoroutine != null)
        {
            StopCoroutine(slideCoroutine);
            slideCoroutine = null;
        }

        HideMessageImmediately();
    }

    private void HideMessageImmediately()
    {
        if (messageText != null)
        {
            messageText.gameObject.SetActive(false);
        }

        if (messageBackground != null)
        {
            messageBackground.SetActive(false);
        }

        if (interactionButton != null)
        {
            interactionButton.interactable = true; 
        }

        isMessageDisplayed = false;
    }

    private IEnumerator CloseMessageAfterCountdown()
    {
        float countdown = leaveCountdown;
        while (countdown > 0f)
        {
            countdown -= Time.deltaTime;
            yield return null;

           
            if (isPlayerInRange)
            {
                yield break;
            }
        }

        HideMessage();
        countdownCoroutine = null;
    }

    private void InteractWithPlayer()
    {
        if (messages.Length > 0)
        {
            string randomMessage = messages[Random.Range(0, messages.Length)];
            Debug.Log("NPC says: " + randomMessage);

            ShowMessage(randomMessage);
        }
        else
        {
            Debug.Log("NPC has nothing to say.");
        }
    }

    private void DefaultButtonFunction()
    {
        Debug.Log("Default button function executed.");
    }

    private IEnumerator SlideDownMessageAndBackground()
    {
        Vector3 initialPosition = new Vector3(messageText.transform.position.x, messageText.transform.position.y + 400f, messageText.transform.position.z);
        Vector3 targetPosition = new Vector3(messageText.transform.position.x, messageText.transform.position.y, messageText.transform.position.z);

        messageText.transform.position = initialPosition;
        messageBackground.transform.position = initialPosition;

        isMessageDisplayed = true;

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

        if (interactionButton != null)
        {
            interactionButton.interactable = true; 
        }
    }

    private void OnDrawGizmosSelected()
    {

        // visual guide para sa range
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, interactionRange);
    }
}
