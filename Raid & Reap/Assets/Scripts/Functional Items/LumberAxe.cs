using System.Collections;
using UnityEngine;

public class LumberAxe : MonoBehaviour
{
    PlayerAnimations playerAnimations;
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

    public void UseRustyLumberAxe()
    {
        // Only swing if not already swinging and cooldown has expired
        if (!isSwinging && timeBetweenSwing <= 0)
        {
            isSwinging = true; // Block further input
            timeBetweenSwing = startTimeBetweenSwing; // Reset cooldown
            playerAnimations.AnimateRustyLumberAxe();

            StartCoroutine(PerformSwing());
        }
    }

    private IEnumerator PerformSwing()
    {
        // Wait for animation or swing delay (synchronized with animation)
        yield return new WaitForSeconds(startTimeBetweenSwing / 2); // Adjust timing to match animation

        // Detect and apply damage to trees
        Collider2D[] treeToDestroy = Physics2D.OverlapCircleAll(swingPositiion.position, swingRange, WhatToDestroy);
        for (int i = 0; i < treeToDestroy.Length; i++)
        {
            treeToDestroy[i].GetComponent<TreeScript>().TakeDamage(toolDamage);
        }

        // Wait for the remainder of the cooldown, if necessary
        yield return new WaitForSeconds(startTimeBetweenSwing / 2);

        playerAnimations.StopAnimation();
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(swingPositiion.position, swingRange);
    }
}