using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAnimations : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    public Vector2 lastDirection;

    public Animator movementAnimator; 
    public Animator cuttingAnimator;  


    public Sprite idleDown;
    public Sprite idleUp;
    public Sprite idleLeft;
    public Sprite idleRight;

    public int damage = 1; 
    public Button cutTreeButton; 
    private Tree nearbyTree;


    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        cutTreeButton.gameObject.SetActive(true); 
        cutTreeButton.onClick.AddListener(CutTree); 

        movementAnimator = GetComponent<Animator>(); 
        cuttingAnimator = GetComponentInChildren<Animator>(); 
    }

    
    void Update()
    {
        AnimateMovement(lastDirection); 
    }

    
    public void AnimateMovement(Vector2 direction)
    {
        if (movementAnimator != null)
        {
            if (direction.magnitude > 0.1f)  
            {
                movementAnimator.SetBool("isMoving", true);
                movementAnimator.SetFloat("horizontal", direction.x);
                movementAnimator.SetFloat("vertical", direction.y);
                lastDirection = direction; 
            }
            else
            {
                movementAnimator.SetBool("isMoving", false);
                UpdateSprite(); 
            }
        }

        
    }

    private void UpdateSprite()
    {
        
        if (lastDirection.magnitude == 0)
        {
            return;
        }

        
        Vector2 normalizedDirection = lastDirection.normalized;

        
        if (Mathf.Abs(normalizedDirection.x) >= Mathf.Abs(normalizedDirection.y))
        {
          
            if (normalizedDirection.x > 0)
            {
                spriteRenderer.sprite = idleRight; 
            }
            else
            {
                spriteRenderer.sprite = idleLeft; 
            }
        }
        else
        {
           
            if (normalizedDirection.y > 0)
            {
                spriteRenderer.sprite = idleUp; 
            }
            else
            {
                spriteRenderer.sprite = idleDown; 
            }
        }
    }

 
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Tree"))
        {
            nearbyTree = collision.GetComponent<Tree>();
            if (nearbyTree != null)
            {
        
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Tree"))
        {
            nearbyTree = null;
        
        }
    }


    void CutTree()
    {
        if (nearbyTree != null)
        {

            if (cuttingAnimator != null)
            {
                ResetCuttingBools();

                string movementDirection = GetMovementDirection(lastDirection);

                switch (movementDirection)
                {
                    case "Right":
                        cuttingAnimator.SetBool("CutRight", true);
                        break;
                    case "Left":
                        cuttingAnimator.SetBool("CutLeft", true);
                        break;
                    case "Up":
                        cuttingAnimator.SetBool("CutUp", true);
                        break;
                    case "Down":
                        cuttingAnimator.SetBool("CutDown", true);
                        break;
                    default:
                        break;
                }
            }

            StartCoroutine(DelayedTreeDamage());
        }
    }

 
    void ResetCuttingBools()
    {
        cuttingAnimator.SetBool("CutRight", false);
        cuttingAnimator.SetBool("CutLeft", false);
        cuttingAnimator.SetBool("CutUp", false);
        cuttingAnimator.SetBool("CutDown", false);
    }

  
    string GetMovementDirection(Vector3 direction)
    {
        Vector3 normalizedDirection = direction.normalized;

        if (Mathf.Abs(normalizedDirection.x) > Mathf.Abs(normalizedDirection.y))
        {
            return normalizedDirection.x > 0 ? "Right" : "Left";
        }
        else
        {
            return normalizedDirection.y > 0 ? "Up" : "Down";
        }
    }

   
    IEnumerator DelayedTreeDamage()
    {
        yield return new WaitForSeconds(0.5f); 
        nearbyTree.TakeDamage(damage);

        ResetCuttingBools(); 
    }
}