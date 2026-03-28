using UnityEngine;

public class DestroyPlatform : MonoBehaviour
{
    [Header("Settings")]
    public float delayBeforeDestroy = 1f;

    private bool isDestroying = false;

    void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        if (isDestroying) return;

        isDestroying = true;
        Invoke(nameof(DestroyPlatform2D), delayBeforeDestroy);
    }

    void DestroyPlatform2D()
    {
        Destroy(gameObject);
    }
}