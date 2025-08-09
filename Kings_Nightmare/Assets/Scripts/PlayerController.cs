using UnityEngine;
using System.Collections;


[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Collider2D))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    // this transform is used to check if the player is grounded only needed if the ground check position is generated and a seperate object
    //private Transform groundCheckPos; - Testing diff ground check Pos solutions 

    private int _score = 0;
    private int _lives = 3;
    private int jumpForce = 6;

    private Coroutine jumpForceChange = null;

    public void ActivateJumpForceChange()
    {
        //start a coroutine that changes the jump force for x seconds
        if (jumpForceChange != null)
        {
            StopCoroutine(jumpForceChange);
            jumpForceChange = null;
            jumpForce = 6; // Reset to default jump force
        }

        jumpForceChange = StartCoroutine(ChangeJumpForce()); // Change jump force to 12 for 3 seconds
    }
    private IEnumerator ChangeJumpForce()
    {
        jumpForce = 12; // Set new jump force
        Debug.Log($"Jump force change to {jumpForce} at {Time.time}");
        yield return new WaitForSeconds(5f); // Wait for 5 seconds
        jumpForce = 6; // Reset to default jump force
        Debug.Log($"Jump force change to {jumpForce} at {Time.time}");
        jumpForceChange = null; // Clear the coroutine reference
    }

    public int score
    {
        get => _score;
        set
        {
            if (value < 0)
                _score = 0;
            else
                _score = value;
        }
    }

    public int lives
    {
        get => _lives;
        set
        {
            if (value < 0)
            {
                //gameover goes here
                Debug.Log("Game Over! You have no lives left.");
                _lives = 0;
            }
            else if (value > maxLives)
            {
                _lives = maxLives;
            }
            else
            {
                _lives = value;
            }
        }
    }

    public int maxLives = 9;

    [SerializeField] private float groundCheckRadius = 0.02f; // Radius for ground check, adjust as necessary
    [SerializeField] private bool attack1 = false;
    [SerializeField] private bool isGrounded = false;
    private LayerMask groundLayer;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Collider2D col;
    private Animator anim;
    private GroundCheck groundCheck;
    private float initialGroundCheckRadius;

    [SerializeField] private int maxJumpCount = 2; // Maximum number of jumps allowed (e.g., double jump)
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
        { 
            Debug.LogWarning("Ground layer not set. Please set the Ground layer in the LayerMask.");
        }
        groundCheck = new GroundCheck(col, groundLayer, groundCheckRadius);
        initialGroundCheckRadius = groundCheckRadius;

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
        float vValue = Input.GetAxisRaw("Vertical");
        SpriteFlip(hValue);
        //Debug.Log("Ground Check Position: " + groundCheckPos);

        rb.linearVelocityX = hValue * 5f; // Adjust speed as necessary
        groundCheck.CheckIsGrounded();
        isGrounded = groundCheck.IsGrounded;


        // jump input check
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
        anim.SetBool("isGrounded", groundCheck.IsGrounded);
        anim.SetBool("attack1", attack1);
        //Debug.Log($"Ground Check Radius from Player object: {groundCheckRadius}");
        if (initialGroundCheckRadius != groundCheckRadius)
            groundCheck.UpdateGroundCheckRadius(groundCheckRadius);
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

    private void OnCollisionEnter2D(Collision2D collision)
    {

    }

    private void OnCollisionExit2D(Collision2D collision)
    {

    }

    private void OnCollisionStay2D(Collision2D collision)
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

    }

    private void OnTriggerExit2D(Collider2D collision)
    {

    }

    private void OnTriggerStay2D(Collider2D collision)
    {

    }
}
