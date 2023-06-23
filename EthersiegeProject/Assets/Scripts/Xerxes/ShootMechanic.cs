using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootMechanic : MonoBehaviour
{
    public GameObject bulletPrefab; // Prefab for the bullet object
    public Transform bulletSpawnPoint; // Point where the bullet should be spawned
    public float bulletSpeed = 10f; // Speed at which the bullet moves
    public float fireRate = 0.2f; // Adjustable fire rate (in seconds)
    public GameObject destructionEffectPrefab; // Particle effect prefab for destruction


    private bool isShooting = false; // Flag to track if the player is currently shooting
    private float timeSinceLastShot = 0f; // Time since the last shot

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Change 0 to the button index you want to use for shooting (e.g., 1 for right mouse button)
        {
            isShooting = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isShooting = false;
        }

        if (isShooting)
        {
            timeSinceLastShot += Time.deltaTime;

            if (timeSinceLastShot >= fireRate)
            {
                Shoot();
                timeSinceLastShot = 0f;
            }
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();

        if (bulletRigidbody != null)
        {
            bulletRigidbody.velocity = bulletSpawnPoint.forward * bulletSpeed;
        }

        // Add a script to the bullet object that handles collision
        bullet.AddComponent<BulletCollisionHandler>().SetDestructionEffectPrefab(destructionEffectPrefab);

        Destroy(bullet, 3f); // Destroy the bullet after 3 seconds (adjust as needed)
    }

    public class BulletCollisionHandler : MonoBehaviour
    {
        private GameObject destructionEffectPrefab; // Particle effect prefab for destruction

        public void SetDestructionEffectPrefab(GameObject effectPrefab)
        {
            destructionEffectPrefab = effectPrefab;
        }

        // This method is called when a collision occurs
        private void OnCollisionEnter(Collision collision)
        {
            // Instantiate destruction effect at the bullet's position
            if (destructionEffectPrefab != null)
            {
                Instantiate(destructionEffectPrefab, transform.position, transform.rotation);
            }

            // Destroy the bullet
            Destroy(gameObject); // Destroy the bullet
        }
    }

}
