using UnityEngine;
using System.Collections;

public class ToggleObjectOnHit : MonoBehaviour
{
    [Header("Object to Toggle")]
    public GameObject targetObject;

    [Header("Settings")]
    public float activeTime = 2f;
    public float inactiveTime = 2f;
    public int loopCount = 0; // 0 = loop forever

    private bool hasStarted = false;

    void Start()
    {
        if (targetObject != null)
            targetObject.SetActive(true); // Start ACTIVE
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        if (hasStarted) return;

        hasStarted = true;
        StartCoroutine(ToggleLoop());
        Debug.Log("Toggle started!");
    }

    IEnumerator ToggleLoop()
    {
        int count = 0;

        while (loopCount == 0 || count < loopCount)
        {
            // Active -> Inactive
            targetObject.SetActive(true);
            Debug.Log(targetObject.name + " ACTIVE");
            yield return new WaitForSeconds(activeTime);

            // Inactive -> Active
            targetObject.SetActive(false);
            Debug.Log(targetObject.name + " INACTIVE");
            yield return new WaitForSeconds(inactiveTime);

            count++;
        }

        // End on active
        targetObject.SetActive(true);
        Debug.Log("Toggle loop finished!");
    }
}