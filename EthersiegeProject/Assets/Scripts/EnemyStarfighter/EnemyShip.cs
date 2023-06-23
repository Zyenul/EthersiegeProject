using UnityEngine;

public class EnemyShip : MonoBehaviour
{
    public Transform player;
    public GameObject projectilePrefab;
    public float movementSpeed = 5f;
    public float stoppingDistance = 5f; // Distance to maintain from the player
    public float fireRate = 2f;
    public float projectileSpeed = 10f;
    public GameObject destructionEffectPrefab; // Particle effect prefab for destruction

    public Transform projectileSpawnPoint1;
    public Transform projectileSpawnPoint2;

    private Rigidbody rb;
    private float fireCooldown;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        fireCooldown = fireRate;
    }

    private void Update()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        float distance = Vector3.Distance(transform.position, player.position);

        // Check if the enemy is within the stopping distance
        if (distance > stoppingDistance)
        {
            // Move the enemy towards the player
            rb.velocity = direction * movementSpeed;
        }
        else
        {
            // Stop the enemy's movement
            rb.velocity = Vector3.zero;
        }

        // Rotate the enemy to face the movement direction
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 360f * Time.deltaTime);
        }

        // Update the firing cooldown
        fireCooldown -= Time.deltaTime;

        // Check if it's time to fire
        if (fireCooldown <= 0f)
        {
            FireProjectile1();
            FireProjectile2();
            fireCooldown = fireRate;
        }
    }

    private void FireProjectile1()
    {
        // Instantiate the projectile
        GameObject projectile1 = Instantiate(projectilePrefab, projectileSpawnPoint1.position, Quaternion.identity);


        // Set the projectile's direction and rotation
        Vector3 direction = (player.position - transform.position).normalized;
        projectile1.GetComponent<Rigidbody>().velocity = direction * projectileSpeed;
        projectile1.transform.rotation = Quaternion.LookRotation(direction);

        Destroy(projectile1, 3f); // Destroy the bullet after 3 seconds (adjust as needed)

        projectile1.AddComponent<BulletCollisionHandler>().SetDestructionEffectPrefab(destructionEffectPrefab);
    }


    private void FireProjectile2()
    {
        // Instantiate the projectile
        GameObject projectile2 = Instantiate(projectilePrefab, projectileSpawnPoint2.position, Quaternion.identity);


        // Set the projectile's direction and rotation
        Vector3 direction = (player.position - transform.position).normalized;
        projectile2.GetComponent<Rigidbody>().velocity = direction * projectileSpeed;
        projectile2.transform.rotation = Quaternion.LookRotation(direction);

        Destroy(projectile2, 3f); // Destroy the bullet after 3 seconds (adjust as needed)

        projectile2.AddComponent<BulletCollisionHandler>().SetDestructionEffectPrefab(destructionEffectPrefab);

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
