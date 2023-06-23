using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsterDebris : MonoBehaviour
{
    public int maxHealth = 100;
    public string projectileTag = "ProjectileXerxes";
    public int hitsToDestroy = 5;
    public int projectileDamage = 20;
    public ParticleSystem damagedParticleSystem;
    public GameObject spawnObjectPrefab;
    public Transform spawnPosition;
    public float spawnObjectDuration = 7f;

    private int currentHealth;
    private int remainingHits;


    private void Start()
    {
        currentHealth = maxHealth;
        remainingHits = hitsToDestroy;
        damagedParticleSystem = GetComponent<ParticleSystem>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(projectileTag))
        {
            // Reduce health when collided with a projectile
            TakeDamage(projectileDamage);

            // Destroy the projectile
            Destroy(collision.gameObject);

            Debug.Log("Asteroid Hit!");

            // Play hit particles
            if (damagedParticleSystem != null)
            {
                damagedParticleSystem.Play();
            }
        }

    }

    private void TakeDamage(int damageAmount)
    {
        // Reduce health by the damage amount
        currentHealth -= damageAmount;

        // Check if the enemy is destroyed
        if (currentHealth <= 0)
        {
            remainingHits--;
            if (remainingHits <= 0)
            {
                // Spawn a new object at the specified position and rotation
                GameObject spawnedObject = Instantiate(spawnObjectPrefab, spawnPosition.position, spawnPosition.rotation);
                Destroy(spawnedObject, spawnObjectDuration);
                Destroy(gameObject);
            }
            else
            {
                // Reset the health for the next hit
                currentHealth = maxHealth;
            }
        }
    }
}
