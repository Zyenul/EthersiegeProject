using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth2 : MonoBehaviour
{
    public int maxHealth = 100;
    public string projectileTag = "ProjectileEnemy";
    public int hitsToDestroy = 5;
    public int projectileDamage = 20;
    public ParticleSystem damagedParticleSystem;
    public GameObject spawnObjectPrefab;
    public Transform spawnPosition;
    public float spawnObjectDuration = 5f;
    public ShipController shipController;

    private int currentHealth;
    private int remainingHits;

    [SerializeField] private HealthBar _healthBar;


    private void Start()
    {
        currentHealth = maxHealth;
        remainingHits = hitsToDestroy;
        damagedParticleSystem = GetComponent<ParticleSystem>();
        _healthBar.UpdateHealthBar(maxHealth, currentHealth);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(projectileTag))
        {
            // Reduce health when collided with a projectile
            TakeDamage(projectileDamage);

            Debug.Log("Received Damage!");

            // Destroy the projectile
            Destroy(collision.gameObject);

            _healthBar.UpdateHealthBar(maxHealth, currentHealth);


            // Play hit particles
            if (damagedParticleSystem != null)
            {
                damagedParticleSystem.Play();
            }
        }

        if (collision.gameObject.CompareTag("Barrier")) // Replace "Obstacle" with the appropriate tag for the colliding object
        {
            //Die(); // Call the Die() method when colliding with the specified object
            // Reduce health when collided with a projectile
            TakeDamage(projectileDamage);

            Debug.Log("Received Damage!");

            _healthBar.UpdateHealthBar(maxHealth, currentHealth);
        }

        if (collision.gameObject.CompareTag("Mine")) // Replace "Obstacle" with the appropriate tag for the colliding object
        {
            //Die(); // Call the Die() method when colliding with the specified object
            // Reduce health when collided with a projectile
            Die();

            Debug.Log("Received Damage!");


            _healthBar.UpdateHealthBar(maxHealth, currentHealth);
        }

        if (collision.gameObject.CompareTag("ObstacleBig")) // Replace "Obstacle" with the appropriate tag for the colliding object
        {
            //Die(); // Call the Die() method when colliding with the specified object
            // Reduce health when collided with a projectile
            Die();

            Debug.Log("Received Damage!");


            _healthBar.UpdateHealthBar(maxHealth, currentHealth);
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
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                shipController.enabled = false;

            }
            else
            {
                // Reset the health for the next hit
                currentHealth = maxHealth;
            }

        }
    }

    public void AddHealth(int amount)
    {
        currentHealth += amount;

        // Ensure the current health doesn't exceed the maximum health
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        // Update the health bar UI
        _healthBar.UpdateHealthBar(maxHealth, currentHealth);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("HealZone"))
        {
            // Increase the player's health by a certain amount
            AddHealth(20);

            // Destroy the health power-up object
            //Destroy(other.gameObject);
        }
    }

    private void Die()
    {
        // Add any necessary logic when the player dies (e.g., play death animation, show game over screen, etc.)
        // In this example, we simply deactivate the player object
        gameObject.SetActive(false);

        GameObject spawnedObject = Instantiate(spawnObjectPrefab, spawnPosition.position, spawnPosition.rotation);
        Destroy(spawnedObject, spawnObjectDuration);
        Destroy(gameObject);
        shipController.enabled = false;
        _healthBar.UpdateHealthBar(maxHealth, currentHealth);
        //SceneManager.LoadScene(sceneName); // Load the specified scene
    }
}
