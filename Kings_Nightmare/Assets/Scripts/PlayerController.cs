using TMPro;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Collider2D))] // ensures that rb component is present and is great defenseive programming and ensures we have access to our physics components 

public class PlayerController : MonoBehaviour
{
    //[Serializefield] private bool isGrounded = false; // check if player is grounded (checks every frame)
    private LayerMask groundLayer;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Collider2D col;
    private GroundCheck groundCheck;

    [SerializeField] private int maxJump = 1; // Maximum number of jumps allowed
    private int jumpCount = 1;

    private Vector2 groundCheckPos;
    Vector2 GetGroundCheckPos() => new Vector2(col.bounds.min.x + col.bounds.extents.x, col.bounds.min.y);



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //rb = TryGetComponent<Rigidbody2D>(out rb); // good for checking if rb exists 
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();

        groundLayer = LayerMask.GetMask("Ground"); // can be used for checks against the ground layer

            //if (rb != null) Debug.Log($"RigidBody Exists {rb.name}"); // debug for console to check if rb exists on object and the name of object 
                //rb.gravityScale = 0f; // disables gravity to check for sprite rb

        //Initialize ground check position if using a separate GameObject for ground checking, such as a child of the player GameObject
            //GameObject newObj = new GameObject("GroundCheck");
            //newObj.transform.SetParent(transform);
            //newObj.transform.localPosition = Vector3.zero; // Set to player's Position
            //groundCheckPos = newObj.transform;
    }

    // Update is called once per frame
    void Update()
    {
        float hValue = Input.GetAxis("Horizontal");
        SpriteFlip(hValue);
        groundCheckPos = GetGroundCheckPos();

        rb.linearVelocityX = hValue * 5f; // Adjust Speed as necessary here

        if (Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector2.up * 10f, ForceMode2D.Impulse); //Adjust jump for as necessary here
        }
    }

    void SpriteFlip(float hValue)
    {
            //if (hValue < 0)
            //    sr.flipX = true;
            //else if (hValue > 0)
            //    sr.flipX = false; //   this "if else statement" checks for flipped sprite every single frame 

        if (hValue != 0) sr.flipX = (hValue < 0); // simplified sprite flipping logic, doesn't check every frame
            // ^^if at the start fixes flip snap back

            //if ((hValue > 0 && sr.flipX) || (hValue < 0 && !sr.flipX))
            //    sr.flipX = !sr.flipX; 
            // flip sprite only when direction changes - only when neccesary 
    }
}
