using System.Collections;
using UnityEngine;

public class LumberAxe : MonoBehaviour
{
    private float timeBetweenSwing;
    public float startTimeBetweenSwing;

    public Transform swingPositiion;
    public LayerMask WhatToDestroy;
    public float swingRange;
    public int toolDamage;
    public int weaponDamage;

    private void Update()
    {
        // Ensure that timeBetweenSwing never goes below 0
        if (timeBetweenSwing > 0)
        {
            timeBetweenSwing -= Time.deltaTime;
        }
    }

    public void UseRustyLumberAxe()
    {
        // Only use the axe if the cooldown time is done
        if (timeBetweenSwing <= 0)
        {
            // Detect trees within the swing range
            Collider2D[] treeToDestroy = Physics2D.OverlapCircleAll(swingPositiion.position, swingRange, WhatToDestroy);
            for (int i = 0; i < treeToDestroy.Length; i++)
            {
                // Apply damage to each tree
                treeToDestroy[i].GetComponent<TreeScript>().TakeDamage(toolDamage);
            }

            // Reset the cooldown timer
            timeBetweenSwing = startTimeBetweenSwing;
        }
        else
        {
            // Reduce the cooldown time by the time passed since the last frame
            timeBetweenSwing -= Time.deltaTime;
        }
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(swingPositiion.position, swingRange);
    }
}