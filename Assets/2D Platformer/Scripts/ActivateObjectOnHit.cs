using UnityEngine;
using System.Collections;

public class ActivateObjectOnHit : MonoBehaviour
{
    [Header("Object to Activate")]
    public GameObject targetObject;

    [Header("Settings")]
    public float delayBeforeActivate = 2f;

    void Start()
    {
        if (targetObject != null)
            targetObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        if (targetObject == null)
        {
            Debug.LogWarning("Target object is not assigned!");
            return;
        }

        StartCoroutine(ActivateAfterDelay());
    }

    IEnumerator ActivateAfterDelay()
    {
        Debug.Log("Activating in " + delayBeforeActivate + " seconds...");

        yield return new WaitForSeconds(delayBeforeActivate);

        targetObject.SetActive(true);
        Debug.Log(targetObject.name + " activated!");
    }
}