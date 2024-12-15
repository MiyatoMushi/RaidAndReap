using UnityEngine;

public class LineOfSightController : MonoBehaviour
{
    public Transform lineOfSight; // Reference to the collider
    public float offsetDistance = 1.0f; // Distance from the player

    private Rigidbody2D rb;

    void Start()
    {
        // Get the Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Get the velocity of the player
        Vector2 velocity = rb.velocity;

        // Determine facing direction based on velocity
        if (velocity.magnitude > 0) // If there's any movement
        {
            // Normalize to get the direction of movement
            velocity = velocity.normalized;

            // Check if the horizontal component is greater than the vertical component
            if (Mathf.Abs(velocity.x) > Mathf.Abs(velocity.y))
            {
                // Horizontal movement
                if (velocity.x > 0)
                {
                    SetLineOfSight(new Vector3(0.5f, 0, 0)); // Facing right
                }
                else
                {
                    SetLineOfSight(new Vector3(-0.5f, 0, 0)); // Facing left
                }
            }
            else
            {
                // Vertical movement
                if (velocity.y > 0)
                {
                    SetLineOfSight(new Vector3(0, 0.5f, 0)); // Facing up
                }
                else
                {
                    SetLineOfSight(new Vector3(0, -0.5f, 0)); // Facing down
                }
            }
        }
    }

    private void SetLineOfSight(Vector3 direction)
    {
        // Update the local position of the lineOfSight object
        if (lineOfSight != null)
        {
            lineOfSight.localPosition = direction * offsetDistance;
        }
        else
        {
            Debug.LogError("LineOfSight is not assigned in the Inspector!");
        }
    }
}