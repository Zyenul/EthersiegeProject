using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnTrigger : MonoBehaviour
{
    [SerializeField] private GameObject enemyToActivate; // The enemy GameObject to activate

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ActivateEnemy();
        }
    }

    private void ActivateEnemy()
    {
        enemyToActivate.SetActive(true);
    }
}
