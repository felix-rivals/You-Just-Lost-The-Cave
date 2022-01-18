using UnityEngine;

public class PlayerScript : MonoBehaviour {

    public float moveSpeed;
    public float jumpHeight;

    private float dashSpeed = 6f; // la vitesse du dash
    private float activeSpeed; // stockera la vitesse de base OU du dash selon si la touche shift est enfoncée ou non

    private Rigidbody2D rb;
    private float moveInput;

    public bool isColliding;
    private bool isGrounded = true;

    private int jumps;
    public bool canDoubleJump = true; // pour plus tard : si on a débloqué le double saut ou pas

    public float dashLength = .1f, dashCooldown = 1f;

    private float dashCounter;
    private float dashCoolCounter;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        activeSpeed = moveSpeed;
    }

    void FixedUpdate()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * activeSpeed, rb.velocity.y);
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space) && (isGrounded || jumps > 0)) // Si la touche espace est enfoncée & qu'il est sur le sol OU qu'il lui reste un double saut
        {
            Jump();
            jumps--;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && (moveInput!=0))
        {
            
            if (dashCoolCounter <= 0 && dashCounter <= 0)
            {
                jumps = 0;
                rb.velocity = Vector3.zero;
                rb.gravityScale = 0f;
                activeSpeed = moveSpeed * dashSpeed;
                dashCounter = dashLength;
            }
        }

        if (dashCounter > 0)
        {
            dashCounter -= Time.deltaTime;

            if (dashCounter < 0)
            {
                rb.gravityScale = 2.8f;
                activeSpeed = moveSpeed;
                dashCoolCounter = dashCooldown;
            }
        }

        if (dashCoolCounter > 0)
        {
            dashCoolCounter -= Time.deltaTime;
        }

    }

    void Jump()
    {
        rb.AddForce(new Vector2(0, jumpHeight), ForceMode2D.Impulse);
    }


    void OnCollisionEnter2D(Collision2D col)
    {
        isGrounded = true;
        if (canDoubleJump)
        {
            jumps = 2;
        }
        else
        {
            jumps = 1;
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (jumps == 2)
        {
            jumps--;
        }
        isGrounded = false;
    }
}