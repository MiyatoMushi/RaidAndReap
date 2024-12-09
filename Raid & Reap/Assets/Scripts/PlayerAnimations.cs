using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAnimations : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    public Vector2 lastDirection;

    //Animations and Sprites
    public Animator animator;
    public Sprite idleDown;
    public Sprite idleUp;
    public Sprite idleLeft;
    public Sprite idleRight;

    public int damage = 1; // Damage dealt by the axe
    public Button cutTreeButton; // UI Button for cutting trees
    private Tree nearbyTree; // Reference to the tree the player is near

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Functions
    public void AnimateMovement(Vector2 direction)
    {
        if (animator != null)
        {
            if (direction.magnitude > 0)
            {
                animator.enabled = true;
                animator.SetBool("isMoving", true);

                animator.SetFloat("horizontal", direction.x);
                animator.SetFloat("vertical", direction.y);

                lastDirection = direction; //take last position
            }
            else
            {
                // Debug.Log("Idle with lastDirection: " + lastDirection); // For checking

                animator.SetBool("isMoving", false);

                animator.enabled = false; // Disable animator to stop animations
                UpdateSprite();
            }
        }
    }

    private void UpdateSprite()
    {
        // Do nothing if there's no last direction
        if (lastDirection.magnitude == 0)
            return;

        // Normalize the last direction to determine the primary direction
        Vector2 normalizedDirection = lastDirection.normalized;

        // Determine the idle sprite based on lastDirection's values
        if (Mathf.Abs(normalizedDirection.x) >= Mathf.Abs(normalizedDirection.y))
        {
            // If Horizontal movement is dominant
            if (normalizedDirection.x > 0)
            {
                spriteRenderer.sprite = idleRight; // Facing right
            }
            else
            {
                spriteRenderer.sprite = idleLeft; // Facing left
            }
        }
        else
        {
            // If Vertical movement is dominant
            if (normalizedDirection.y > 0)
            {
                spriteRenderer.sprite = idleUp; // Facing up
            }
            else
            {
                spriteRenderer.sprite = idleDown; // Facing down
            }
        }
    }
}