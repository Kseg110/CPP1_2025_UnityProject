using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))] // ensures that rb component is present and is great defenseive programming and ensures we have access to our physics components 

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rb;
    private SpriteRenderer sr;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //rb = TryGetComponent<Rigidbody2D>(out rb); // good for checking if rb exists 
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();


        //if (rb != null) Debug.Log($"RigidBody Exists {rb.name}"); // debug for console to check if rb exists on oobject and the name of object 

        //rb.gravityScale = 0f; // disables gravity to check for sprite rb
    }

    // Update is called once per frame
    void Update()
    {
        float hValue = Input.GetAxis("Horizontal");
        SpriteFlip(hValue);

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
