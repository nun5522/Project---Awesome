using UnityEngine;
using System.Collections;

public class MoveObjectOnHit2D : MonoBehaviour
{
    [Header("Object to Move")]
    public GameObject targetObject;

    [Header("Move Settings")]
    public Vector2 moveDirection = new Vector2(1, 0);
    public float moveSpeed = 3f;
    public float moveDistance = 5f;
    public float delayBeforeMove = 2f;

    private bool isMoving = false;
    private Vector2 targetPos;

    void Update()
    {
        if (!isMoving || targetObject == null) return;

        targetObject.transform.position = Vector2.MoveTowards(
            targetObject.transform.position,
            targetPos,
            moveSpeed * Time.deltaTime
        );

        if ((Vector2)targetObject.transform.position == targetPos)
            isMoving = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        if (targetObject == null)
        {
            Debug.LogWarning("Target object is not assigned!");
            return;
        }

        StartCoroutine(MoveAfterDelay());
    }

    IEnumerator MoveAfterDelay()
    {
        Debug.Log("Moving in " + delayBeforeMove + " seconds...");

        yield return new WaitForSeconds(delayBeforeMove);

        targetPos = (Vector2)targetObject.transform.position + moveDirection.normalized * moveDistance;
        isMoving = true;

        Debug.Log("Moving: " + targetObject.name);
    }
}