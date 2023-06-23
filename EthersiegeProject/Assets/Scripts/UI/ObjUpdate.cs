using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjUpdate : MonoBehaviour
{
    public GameObject NextObjective;
    public GameObject prevObjText;
    public GameObject nextObjText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            NextObjective.SetActive(true);
            gameObject.SetActive(false);
            prevObjText.SetActive(false);
            nextObjText.SetActive(true);

        }
    }
}
