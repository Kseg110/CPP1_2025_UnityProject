using UnityEngine;

public class TurretEnemy : Enemy
{
    private Transform player;

    [SerializeField] private float fireRate = 20.0f; // Time between shots
    private float timeSinceLastShot = 2.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        // Additional initialization for TurretEnemy can go here

        //GameManager.Instance.OnPlayerControllerCreated += (playerController) => player = playerController.transform;

        if (fireRate <= 0)
        {
            Debug.LogError("Fire rate must be greater than 0. Setting to default value of 2.0f.");
            fireRate = 2.0f;
        }
    }


    public override void TakeDamage(int damageValue, DamageType damageType = DamageType.Default)
    {
        if (damageType == DamageType.Default)
        {
            // Destroy the enemy after the death animation is complete
            Destroy(transform.parent.gameObject, 1f); // Adjust the delay as needed for the animation
            return;
        }

        base.TakeDamage(damageValue, damageType);
    }



    // Update is called once per frame
    void Update()
    {
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.IsName("turret_idle"))
        {
            //check our fire logic
            if (Time.time >= timeSinceLastShot * fireRate)
            {
                anim.SetTrigger("fire");
                timeSinceLastShot = Time.time; // Reset the timer after firing
            }
        }
    }
}