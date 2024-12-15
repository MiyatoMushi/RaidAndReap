using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAnimations : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    private Vector2 lastDirection;
    //private InventoryManager inventoryManager;

    //Animations and Sprites
    public Animator animator;
    //public Sprite idleDown;
    //public Sprite idleUp;
    //public Sprite idleLeft;
    //public Sprite idleRight;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        //inventoryManager = FindObjectOfType<InventoryManager>();
    }

    //Functions
    public void AnimateMovement(Vector2 direction)
    {
        if (animator != null)
        {
            if (direction.magnitude > 0)
            {
                //animator.enabled = true;
                animator.SetBool("isMoving", true);
                animator.SetFloat("horizontal", direction.x);
                animator.SetFloat("vertical", direction.y);
                
                lastDirection = direction; //take last position
                //inventoryManager.DisableActionButton();
                StopAnimation();
            }
            else
            {
                // Debug.Log("Idle with lastDirection: " + lastDirection); // For checking

                animator.SetBool("isMoving", false);
                //inventoryManager.EnableActionButton();
                //animator.enabled = false; // Disable animator to stop animations
                //UpdateSprite();
            }
        }
    }

    //Lumber Axe
    public void AnimateRustyLumberAxe() {
        if (animator != null) {
            animator.SetBool("isMoving", false);
            animator.SetBool("rustyLumberAxe", true);

            animator.SetFloat("horizontal", lastDirection.x);
            animator.SetFloat("vertical", lastDirection.y);
        }
    }

    public void AnimateIronLumberAxe() {
        if (animator != null) {
            animator.SetBool("isMoving", false);
            animator.SetBool("ironLumberAxe", true);

            animator.SetFloat("horizontal", lastDirection.x);
            animator.SetFloat("vertical", lastDirection.y);
        }
    }

    public void AnimateGoldLumberAxe() {
        if (animator != null) {
            animator.SetBool("isMoving", false);
            animator.SetBool("goldLumberAxe", true);

            animator.SetFloat("horizontal", lastDirection.x);
            animator.SetFloat("vertical", lastDirection.y);
        }
    }

    //Sword
    public void AnimateRustySword() {
        if (animator != null) {
            animator.SetBool("isMoving", false);
            animator.SetBool("rustySword", true);

            animator.SetFloat("horizontal", lastDirection.x);
            animator.SetFloat("vertical", lastDirection.y);
        }
    }

    public void AnimateIronSword() {
        if (animator != null) {
            animator.SetBool("isMoving", false);
            animator.SetBool("ironSword", true);

            animator.SetFloat("horizontal", lastDirection.x);
            animator.SetFloat("vertical", lastDirection.y);
        }
    }

    public void AnimateGoldSword() {
        if (animator != null) {
            animator.SetBool("isMoving", false);
            animator.SetBool("goldSword", true);

            animator.SetFloat("horizontal", lastDirection.x);
            animator.SetFloat("vertical", lastDirection.y);
        }
    }

    public void StopAnimation() {
        animator.SetBool("rustyLumberAxe", false);
        animator.SetBool("ironLumberAxe", false);
        animator.SetBool("goldLumberAxe", false);
        animator.SetBool("rustySword", false);
        animator.SetBool("ironSword", false);
        animator.SetBool("goldSword", false);
    }

    /*private void UpdateSprite()
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
    }*/
}