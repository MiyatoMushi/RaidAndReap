using System.Collections;
using UnityEngine;

public class EnemyKnockback : MonoBehaviour
{
    private bool isKnockedBack = false;

    public void ApplyKnockback(Vector2 direction, float distance, float duration)
    {
        if (!isKnockedBack)
        {
            StartCoroutine(KnockbackCoroutine(direction.normalized, distance, duration));
        }
    }

    private IEnumerator KnockbackCoroutine(Vector2 direction, float distance, float duration)
    {
        isKnockedBack = true;

        // Store the initial position to calculate knockback correctly
        Vector2 startPosition = transform.position;
        Vector2 targetPosition = startPosition + direction * distance;

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            transform.position = Vector2.Lerp(startPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the final position is exactly the target position
        transform.position = targetPosition;

        isKnockedBack = false;
    }
}
