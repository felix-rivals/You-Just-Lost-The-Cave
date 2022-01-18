using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    public float speed;
    public float jumpHeight;

    private Rigidbody2D rb;
    private float moveInput;

    public bool isColliding;
    private bool isGrounded = true;

    private int jumps;
    private bool canDoubleJump = false; // pour plus tard : si on a débloqué le double saut ou pas
    

    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate() {
        moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
    }

    void Update() {

        if (Input.GetKeyDown(KeyCode.Space) && (isGrounded || jumps > 0)) // Si la touche espace est enfoncée & qu'il est sur le sol OU qu'il lui reste un double saut
        {
            Jump();
            jumps--;
        }
        
    }

    void Jump() {
        rb.AddForce(new Vector2(0, jumpHeight), ForceMode2D.Impulse);
    }


    void OnCollisionEnter2D(Collision2D col)
    {
        isGrounded = true;
        if (canDoubleJump) {
            jumps = 2;
        } else {
            jumps = 1;
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        isGrounded = false;
    }

}