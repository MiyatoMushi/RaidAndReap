using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    QuestManager questManager;
    public GameObject itemPrefab; // Drop Item Prefab
    public enum EnemyType{
        Boar,
        Slime
    }

    public EnemyType enemyType;

    void Start()
    {
        currentHealth = maxHealth;

        GameObject questManagers = GameObject.FindGameObjectWithTag("QuestManager");

        if (questManagers != null)
        {
            questManager = questManagers.GetComponent<QuestManager>();
            if (questManager == null)
            {
                Debug.LogError("GameObject with tag 'QuestManager' does not have a QuestManager component.");
            }
        }
        else
        {
            Debug.LogError("GameObject with tag 'QuestManager' not found in the scene.");
        }
    }

    public void TakeDamage(int damage)
    {
        
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        switch (enemyType)
        {
            case EnemyType.Boar:
                // Boar-specific death behavior
                questManager.OnKillTarget("Boar");
                // Add any specific logic for the Boar here
                break;

            case EnemyType.Slime:
                // Slime-specific death behavior
                questManager.OnKillTarget("Boar");
                // Add any specific logic for the Slime here
                break;

            default:
                Debug.Log("Unknown enemy type has died.");
                break;
        }
        DropItem();
        Destroy(gameObject); // Or handle death logic here
    }

    void DropItem()
    {
        Instantiate(itemPrefab, transform.position, Quaternion.identity);
    }
}
