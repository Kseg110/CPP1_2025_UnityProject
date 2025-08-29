using UnityEngine;

public class TurretEnemy : Enemy
{
    private Transform player;

    [SerializeField] private float fireRate = 5.0f; // Time between shots
    private float timeSinceLastShot = 0.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        // Additional initialization for TurretEnemy can go here

        GameManager.Instance.OnPlayerControllerCreated += (playerController) => player = playerController.transform;

        if (fireRate <= 0)
        {
            Debug.LogError("Fire rate must be greater than 0. Setting to default value of 2.0f.");
            fireRate = 5.0f;
        }
    }
    // Update is called once per frame
    void Update()
    {
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.IsName("Idle"))
        {
            //check our file logic
            if (Time.time >= timeSinceLastShot + fireRate)
            {
                anim.SetTrigger("Fire");
                timeSinceLastShot = Time.time; // Reset the timer after firing
            }
        }
    }
}