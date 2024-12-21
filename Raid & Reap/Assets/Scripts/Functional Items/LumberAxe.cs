using System.Collections;
using UnityEngine;

public class LumberAxe : MonoBehaviour
{
    private PlayerAnimations playerAnimations;
    private PlayerMovement playerMovement;
    private float timeBetweenSwing;
    public float startTimeBetweenSwing;

    public Transform swingPositiion;
    public LayerMask WhatToDestroy;
    public float swingRange;
    public int toolDamage;

    private bool isSwinging = false; // Prevent spamming

    private void Start()
    {
        playerAnimations = transform.Find("Player_Visuals").GetComponent<PlayerAnimations>();
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    private void Update()
    {
        // Cooldown logic
        if (timeBetweenSwing > 0)
        {
            timeBetweenSwing -= Time.deltaTime;
        }

        // Allow swinging again after cooldown
        if (timeBetweenSwing <= 0 && isSwinging)
        {
            isSwinging = false;
        }
    }

    public void UseLumberAxe(int damage, int axeID)
    {
        // Only swing if not already swinging and cooldown has expired
        if (!isSwinging && timeBetweenSwing <= 0)
        {
            
            Collider2D[] treeToDestroy = Physics2D.OverlapCircleAll(swingPositiion.position, swingRange, WhatToDestroy);
            for (int i = 0; i < treeToDestroy.Length; i++)
            {
                treeToDestroy[i].GetComponent<DestroyableObject>().TakeDamage(damage);
            }
            isSwinging = true; // Block further input
            timeBetweenSwing = startTimeBetweenSwing; // Reset cooldown

            PlayLumberAxeAnimation(axeID);
            playerMovement.DeactivateMovement();
            StartCoroutine(PerformSwing());
        }
    }

    private IEnumerator PerformSwing()
    {
        // Wait for animation or swing delay (synchronized with animation)
        yield return new WaitForSeconds(startTimeBetweenSwing / 1);
        Collider2D[] treeToDestroy = Physics2D.OverlapCircleAll(swingPositiion.position, swingRange, WhatToDestroy);
        for (int i = 0; i < treeToDestroy.Length; i++)
        {
            treeToDestroy[i].GetComponent<DestroyableObject>().IsDestroyed();
        }
        //FindObjectOfType<DestroyableObject>().IsDestroyed();
        playerMovement.ActivateMovement();
        playerAnimations.StopAnimation();
    }

    private void PlayLumberAxeAnimation(int axeID)
    {
        switch (axeID) 
        {
            case 1:
                playerAnimations.AnimateRustyLumberAxe();
                break;
            case 2:
                playerAnimations.AnimateIronLumberAxe();
                break;
            case 3:
                playerAnimations.AnimateGoldLumberAxe();
                break;

        }
    }

    //Gizmo for player range in tools
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(swingPositiion.position, swingRange);
    }
}