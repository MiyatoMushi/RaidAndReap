using UnityEngine;

public class TryFootstep : MonoBehaviour
{
    public LayerMask grassLayer;
    public LayerMask soilLayer;
    public LayerMask woodLayer;

    private string currentGround;

    void Update()
    {
        Vector2 playerPosition = transform.position;

        if (Physics2D.OverlapPoint(playerPosition, grassLayer))
        {
            if (currentGround != "Grass")
            {
                currentGround = "Grass";
                PlayFootstepSound("Grass");
            }
        }
        else if (Physics2D.OverlapPoint(playerPosition, soilLayer))
        {
            if (currentGround != "Soil")
            {
                currentGround = "Soil";
                PlayFootstepSound("Soil");
            }
        }
        else if (Physics2D.OverlapPoint(playerPosition, woodLayer))
        {
            if (currentGround != "Wood")
            {
                currentGround = "Wood";
                PlayFootstepSound("Wood");
            }
        }
    }

    void PlayFootstepSound(string groundType)
    {
        // Replace this with your audio logic
        Debug.Log("Playing " + groundType + " footstep sound");
    }
}