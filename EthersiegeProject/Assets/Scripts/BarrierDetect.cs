using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierDetect : MonoBehaviour
{
    public string targetTag = "BarrierDetector";
    public Animator animator;
    private bool isTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag) && !isTriggered)
        {
            isTriggered = true;
            animator.SetTrigger("playerDetect");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(targetTag) && isTriggered)
        {
            isTriggered = false;
            animator.SetTrigger("playerDetect");
        }
    }
}
