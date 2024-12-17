using UnityEngine;
using UnityEngine.UI;

public class InventoryHover : MonoBehaviour
{
    [Header("UI Panel")]
    public RectTransform inventoryPanel;  // Reference to the shared inventory UI panel

    [Header("Buttons")]
    public Button inventoryButton;        // Button to toggle Main Inventory
    public Button craftButton;            // Button to toggle Craft Inventory

    [Header("Hover Settings")]
    public float hoverHeight = 100f;      // How high the panel hovers
    public float hoverSpeed = 2f;         // Speed of the hover animation

    private Vector2 originalPosition;     // Original position of the panel
    private Vector2 targetPosition;       // Hovered position of the panel
    private bool isPanelVisible = false;  // Tracks if the panel is visible
    private string activeSection = "";    // Tracks the active section

    [Header("Inventory Sections")]
    public GameObject mainInventorySection; // Main Inventory section
    public GameObject craftInventorySection; // Craft Inventory section

    void Start()
    {
        // Store original position and calculate target hover position
        originalPosition = inventoryPanel.anchoredPosition;
        targetPosition = originalPosition + new Vector2(0, hoverHeight);

        // Add button listeners
        inventoryButton.onClick.AddListener(() => ToggleSection("Main"));
        craftButton.onClick.AddListener(() => ToggleSection("Craft"));

        // Initialize with both sections hidden
        mainInventorySection.SetActive(false);
        craftInventorySection.SetActive(false);
    }

    void Update()
    {
        // Detect a mouse click outside the panel to close it
        if (isPanelVisible && Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Input.mousePosition;
            if (!RectTransformUtility.RectangleContainsScreenPoint(inventoryPanel, mousePos))
            {
                ClosePanel();
            }
        }
    }

    void ToggleSection(string sectionName)
    {
        // If the panel is currently visible but a new section is clicked, switch to that section
        if (isPanelVisible && activeSection == sectionName)
        {
            ClosePanel();
            return;
        }

        // Activate the shared panel
        if (!isPanelVisible)
        {
            OpenPanel();
        }

        // Toggle sections
        if (sectionName == "Main")
        {
            mainInventorySection.SetActive(true);
            craftInventorySection.SetActive(false);
        }
        else if (sectionName == "Craft")
        {
            mainInventorySection.SetActive(false);
            craftInventorySection.SetActive(true);
        }

        activeSection = sectionName;
    }

    void OpenPanel()
    {
        isPanelVisible = true;
        StopAllCoroutines();
        StartCoroutine(HoverPanel(targetPosition));
    }

    void ClosePanel()
    {
        isPanelVisible = false;
        activeSection = "";
        StopAllCoroutines();
        StartCoroutine(HoverPanel(originalPosition, () =>
        {
            mainInventorySection.SetActive(false);
            craftInventorySection.SetActive(false);
        }));
    }

    System.Collections.IEnumerator HoverPanel(Vector2 targetPos, System.Action onComplete = null)
    {
        while (Vector2.Distance(inventoryPanel.anchoredPosition, targetPos) > 0.01f)
        {
            inventoryPanel.anchoredPosition = Vector2.Lerp(
                inventoryPanel.anchoredPosition,
                targetPos,
                hoverSpeed * Time.deltaTime
            );
            yield return null; // Wait for the next frame
        }
        inventoryPanel.anchoredPosition = targetPos; // Snap to the target position
        onComplete?.Invoke(); // Invoke optional callback
    }
}
