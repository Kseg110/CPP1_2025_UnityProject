using UnityEngine;

public class Shoot : MonoBehaviour
{
    private SpriteRenderer sr;
    [SerializeField] private Vector2 initShotVelocity = Vector2.zero;
    [SerializeField] private Transform leftShootSpawn;
    [SerializeField] private Transform rightShootSpawn;
    [SerializeField] private Projectile projectilePrefab = null;

    private Vector2 leftShotVel = Vector2.zero;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        if (initShotVelocity == Vector2.zero)
        {
            initShotVelocity = new Vector2(6f, 0f); // Default shot velocity if not set
            Debug.LogWarning("Initial shot velocity not set. Using default value: " + initShotVelocity);
        }

        if (leftShootSpawn == null || rightShootSpawn == null || projectilePrefab == null)
        {
            Debug.LogError("Spawn points or projectile prefab not set. Please assign in the inspector.");
        }
        // Set the left and right shot velocities based on the initial shot velocity
        leftShotVel = new Vector2(-initShotVelocity.x, initShotVelocity.y);
    }

    public void Suriken()
    {
        Projectile curProjectile;
        if (!sr.flipX)
        {
            curProjectile = Instantiate(projectilePrefab, rightShootSpawn.position, Quaternion.identity);
            curProjectile.SetVelocity(initShotVelocity);
        }
        else
        {
            curProjectile = Instantiate(projectilePrefab, leftShootSpawn.position, Quaternion.identity);
            curProjectile.SetVelocity(leftShotVel);
        }
    }
}


