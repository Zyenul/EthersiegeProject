using UnityEngine;
using TMPro;
using System.Collections;

public class WallTurn : MonoBehaviour
{
    public Transform playerTransform;
    public float turnSpeed = 100f;
    public float moveSpeed = 1f;
    public float detectionDistance = 2f;
    public TextMeshProUGUI infoText;
    public float textDisplayDuration = 3f;

    private bool nearWall = false;
    private bool canMove = true;

    void Update()
    {
        if (nearWall && canMove)
        {
            canMove = false;
            StartCoroutine(TurnAndMoveForward());
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("TurnWall"))
        {
            nearWall = true;
            infoText.gameObject.SetActive(true);
            StartCoroutine(DeactivateTextAfterDelay());
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("TurnWall"))
        {
            nearWall = false;
            //infoText.gameObject.SetActive(false);
        }
    }

    IEnumerator TurnAndMoveForward()
    {
        Vector3 targetDirection = playerTransform.forward;

        float turnAngle = 0f;
        while (turnAngle < 180f)
        {
            float turnAmount = turnSpeed * Time.deltaTime;
            turnAngle += turnAmount;

            playerTransform.Rotate(0f, turnAmount, 0f);
            yield return null;
        }

        // After the rotation is complete, move forward until out of the wall area
        while (nearWall)
        {
            playerTransform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
            yield return null;
        }

        canMove = true;
    }

    IEnumerator DeactivateTextAfterDelay()
    {
        yield return new WaitForSeconds(textDisplayDuration);
        infoText.gameObject.SetActive(false);
    }
}