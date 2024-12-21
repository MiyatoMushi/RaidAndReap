using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickaxe : MonoBehaviour
{
    private PlayerAnimations playerAnimations;
    private PlayerMovement playerMovement;
    private float timeBetweenSwing;
    public float startTimeBetweenSwing;

    public Transform swingPositiion;
    public LayerMask WhatToDestroy;
    public float swingRange;

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

    public void UsePickaxe(int damage, int axeID)
    {
        // Only swing if not already swinging and cooldown has expired
        if (!isSwinging && timeBetweenSwing <= 0)
        {
            
            Collider2D[] rockToDestroy = Physics2D.OverlapCircleAll(swingPositiion.position, swingRange, WhatToDestroy);
            for (int i = 0; i < rockToDestroy.Length; i++)
            {
                rockToDestroy[i].GetComponent<DestroyableObject>().TakeDamage(damage);
            }
            isSwinging = true; // Block further input
            timeBetweenSwing = startTimeBetweenSwing; // Reset cooldown

            PlayPickaxeAnimation(axeID);
            playerMovement.DeactivateMovement();
            StartCoroutine(PerformSwing());
        }
    }

    private IEnumerator PerformSwing()
    {
        // Wait for animation or swing delay (synchronized with animation)
        yield return new WaitForSeconds(startTimeBetweenSwing / 1);
        Collider2D[] rockToDestroy = Physics2D.OverlapCircleAll(swingPositiion.position, swingRange, WhatToDestroy);
        for (int i = 0; i < rockToDestroy.Length; i++)
        {
            rockToDestroy[i].GetComponent<DestroyableObject>().IsDestroyed();
        }
        //FindObjectOfType<DestroyableObject>().IsDestroyed();
        playerMovement.ActivateMovement();
        playerAnimations.StopAnimation();
    }

    private void PlayPickaxeAnimation(int axeID)
    {
        switch (axeID) 
        {
            case 1:
                playerAnimations.AnimateRustyPickaxe();
                break;
            case 2:
                playerAnimations.AnimateIronPickaxe();
                break;
            case 3:
                playerAnimations.AnimateGoldPickaxe();
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
