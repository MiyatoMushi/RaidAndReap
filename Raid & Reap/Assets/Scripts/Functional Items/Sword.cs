using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
private PlayerAnimations playerAnimations;
    private PlayerMovement playerMovement;
    private float timeBetweenSwing;
    public float startTimeBetweenSwing;

    public Transform swingPositiion;
    public LayerMask WhatToKill;
    public float swingRange;
    public int weaponDamage;

    private bool isSwinging = false; // Prevent spamming
    private SwordColliderController swordCollider;
    private SwordDamage swordDamager;

    private void Start()
    {
        playerAnimations = transform.Find("Player_Visuals").GetComponent<PlayerAnimations>();

        GameObject swordObject = GameObject.FindGameObjectWithTag("HitBox");
        if (swordObject != null)
        {
            swordCollider = swordObject.GetComponent<SwordColliderController>();
            if (swordCollider == null)
            {
                Debug.LogError("SwordColliderController component not found on GameObject with tag 'Sword'.");
            }
            swordDamager = swordObject.GetComponent<SwordDamage>();
            if (swordDamager == null)
            {
                Debug.LogError("SwordDamage component not found on GameObject with tag 'Sword'.");
            }
        }
        else
        {
            Debug.LogError("No GameObject found with tag 'Sword'.");
        }

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

    public void UseSword(int damage, int swordID)
    {
        // Only swing if not already swinging and cooldown has expired
        if (!isSwinging && timeBetweenSwing <= 0)
        {
            isSwinging = true; // Block further input
            timeBetweenSwing = startTimeBetweenSwing; // Reset cooldown


            PlaySwordAnimation(swordID);
            playerMovement.DeactivateMovement();
            StartCoroutine(PerformSwing(damage));
        }
    }

    private IEnumerator PerformSwing(int damage)
    {
        swordCollider.EnableCollider();

        swordDamager.GetDamage(damage);
        
        // Wait for animation or swing delay (synchronized with animation)
        yield return new WaitForSeconds(startTimeBetweenSwing / 2);
        
        swordCollider.DisableCollider();

        //FindObjectOfType<DestroyableObject>().IsDestroyed();
        playerMovement.ActivateMovement();
        playerAnimations.StopAnimation();
    }

    private void PlaySwordAnimation(int swordID)
    {
        switch (swordID) 
        {
            case 1:
                playerAnimations.AnimateRustySword();
                break;
            case 2:
                playerAnimations.AnimateIronSword();
                break;
            case 3:
                playerAnimations.AnimateGoldSword();
                break;

        }
    }

    //Gizmo for player range in tools
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(swingPositiion.position, swingRange);
    }
}
