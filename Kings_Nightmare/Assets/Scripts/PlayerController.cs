using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Collider2D))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    // this transform is used to check if the player is grounded only needed if the ground check position is generated and a seperate object
    //private Transform groundCheckPos;

    [SerializeField] private float groundCheckRadius = 0.02f; // Radius for ground check, adjust as necessary
    [SerializeField] private bool attack1 = false;
    [SerializeField] private bool isGrounded = false;
    private LayerMask groundLayer;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Collider2D col;
    private Animator anim;

    [SerializeField] private int maxJumpCount = 1; // Maximum number of jumps allowed (e.g., double jump)
    private int jumpCount = 1;

    private Vector2 groundCheckPos => new Vector2(col.bounds.min.x + col.bounds.extents.x, col.bounds.min.y);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();

        groundLayer = LayerMask.GetMask("Ground");

        if (groundLayer == 0)
            Debug.LogWarning("Ground layer not set. Please set the Ground layer in the LayerMask.");

        // Initialize ground check position if using a separate GameObject for ground checking
        //GameObject newObj = new GameObject("GroundCheck");
        //newObj.transform.SetParent(transform);
        //newObj.transform.localPosition = Vector3.zero; // Set to the player's position
        //groundCheckPos = newObj.transform;


    }

    // Update is called once per frame
    void Update()
    {
        float hValue = Input.GetAxisRaw("Horizontal");
        SpriteFlip(hValue);
        //Debug.Log("Ground Check Position: " + groundCheckPos);

        rb.linearVelocityX = hValue * 5f; // Adjust speed as necessary
        isGrounded = Physics2D.OverlapCircle(groundCheckPos, groundCheckRadius, groundLayer);

        if (Input.GetButtonDown("Jump"))
            if (Input.GetButtonDown("Jump") && jumpCount < maxJumpCount)
            {
                rb.AddForce(Vector2.up * 2f, ForceMode2D.Impulse); // Adjust jump force as necessary
                rb.AddForce(Vector2.up * 1f, ForceMode2D.Impulse); // Adjust jump force as necessary
                jumpCount++;
                //Debug.Log("Jump Count: " + jumpCount);
            }

        if (Input.GetButtonDown("Fire1"))
            attack1 = true;

        // Check if attack1 animation is done
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        if (attack1 && stateInfo.IsName("attack1") && stateInfo.normalizedTime >= 1.0f)
        {
            attack1 = false;
        }

        if (isGrounded)
            jumpCount = 1; // Reset jump count when grounded

        // Update animator parameters
        anim.SetFloat("hValue", Mathf.Abs(hValue));
        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("attack1", attack1);
    }

    void SpriteFlip(float hValue)
    {
        //if (hValue < 0)
        //    sr.flipX = true;
        //else if (hValue > 0)
        //    sr.flipX = false;
        if (hValue != 0) sr.flipX = (hValue < 0); // Simplified sprite flipping logic

        //if ((hValue > 0 && sr.flipX) || (hValue < 0 && !sr.flipX))
        //    sr.flipX = !sr.flipX; // Flip sprite only when direction changes
    }

}
