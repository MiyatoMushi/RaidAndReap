using System.Collections;
using UnityEngine;

public class LumberAxe : MonoBehaviour
{
    public void CutTree(GameObject tree)
    {
        // Example logic to damage the tree
        TreeScript treeScript = tree.GetComponent<TreeScript>();
        if (treeScript != null)
        {
            treeScript.TakeDamage(1); // Deal 1 damage to the tree
            Debug.Log("Tree hit with LumberAxe!");

            // If the tree is destroyed, log or play an animation
            if (treeScript.IsDestroyed())
            {
                Debug.Log("Tree destroyed!");
                Destroy(tree);
            }
        }
        else
        {
            Debug.Log("No tree found to cut!");
        }
    }
}