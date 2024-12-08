using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackSmith : MonoBehaviour
{
    public float interactionRange = 0.5f;
    private Transform player;

    public GameObject interactionIcon;
    public Button interactionButton;

    public GameObject shopUI; // Reference to the blacksmith shop UI
    public Sprite defaultButtonIcon;
    public Sprite interactButtonIcon;

    public List<Canvas> canvasesToHide; // List of other canvases to hide

    private bool isPlayerInRange = false;

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

        if (shopUI != null)
        {
            shopUI.SetActive(false); // Ensure the shop UI is hidden initially
        }
        else
        {
            Debug.LogError("Shop UI is not assigned in the Inspector.");
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
                ChangeButtonFunction(OpenShop);
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

                // Close the shop UI if the player moves out of range
                if (shopUI.activeSelf)
                {
                    CloseShop();
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

    private void OpenShop()
    {
        if (shopUI != null)
        {
            // Hide other canvases
            foreach (Canvas canvas in canvasesToHide)
            {
                canvas.gameObject.SetActive(false);
            }

            shopUI.SetActive(true);
            Debug.Log("Shop UI opened.");
        }
    }

    private void CloseShop()
    {
        if (shopUI != null)
        {
            shopUI.SetActive(false);

            // Re-enable other canvases
            foreach (Canvas canvas in canvasesToHide)
            {
                canvas.gameObject.SetActive(true);
            }

            Debug.Log("Shop UI closed.");
        }
    }

    private void DefaultButtonFunction()
    {
        Debug.Log("Default button function executed.");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, interactionRange);
    }
}