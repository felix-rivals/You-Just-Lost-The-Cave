using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    private float defaultGravity = 2.8f;
    public float DefaultmoveSpeed;
    public float jumpHeight;

    private float dashSpeed = 6f; // la vitesse du dash
    private float activeSpeed; // stockera la vitesse de base OU du dash selon si la touche shift est enfoncée ou non

    private Rigidbody2D rb;
    public Camera cam;
    private float moveInput;

    public bool isColliding;
    public bool isGrounded = false;

    public Animator anim;
    public SpriteRenderer spriteR;

    private int jumps;
    public bool canDoubleJump = false; // pour plus tard : si on a débloqué le double saut ou pas

    public bool canDash = false;
    public float dashLength = .1f, dashCooldown = 1f;

    private float dashCounter;
    private float dashCoolCounter;

    public bool canClimbing = false;
    private bool isClimbing = false;

    /*
    private class LadderMovement : MonoBehaviour
    {
        private float vertical;
        private float speed = 2f;
        private bool isLadder;
        private bool isClimbing;

        [SerializeField] private Rigidbody2D rb;

        void Update()
        {
            vertical = Input.GetAxisRaw("Vertical");

            if (isLadder && Mathf.Abs(vertical) > 0f)
            {
                isClimbing = true;
            }
        }

        private void FixedUpdate()
        {
            if (isClimbing)
            {
                rb.gravityScale = 1f;
                rb.velocity = new Vector2(rb.velocity.x, vertical * speed);
            }
            else
            {
                rb.gravityScale = 1f;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Ladder"))
            {
                isLadder = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Ladder"))
            {
                isLadder = false;
                isClimbing = false;
            }
        }
    }
    */

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        DefaultmoveSpeed = 12;
        activeSpeed = DefaultmoveSpeed;
        jumpHeight = 34;
    }

    void FixedUpdate()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * activeSpeed, rb.velocity.y);

        flip(rb.velocity.x);
        float charaVelocity = Mathf.Abs(rb.velocity.x);
        anim.SetFloat("Speed", charaVelocity);
    }

    void Update()
    {

        checkJump();
        checkDash();
        checkCheat();

    }

    private void checkJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && (isGrounded || jumps > 0)) // Si la touche espace est enfoncée & qu'il est sur le sol OU qu'il lui reste un double saut
        {
            rb.velocity = Vector3.zero;
            Jump();
            jumps--;
        }
    }

    private void checkDash()
    {
        if (canDash)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) && (moveInput != 0))
            {

                if (dashCoolCounter <= 0 && dashCounter <= 0)
                {
                    rb.velocity = Vector3.zero;
                    rb.gravityScale = 0f;
                    activeSpeed = DefaultmoveSpeed * dashSpeed;
                    dashCounter = dashLength;
                }
            }

            if (dashCounter > 0)
            {
                dashCounter -= Time.deltaTime;

                if (dashCounter < 0)
                {
                    rb.gravityScale = defaultGravity;
                    activeSpeed = DefaultmoveSpeed;
                    dashCoolCounter = dashCooldown;
                }
            }

            if (dashCoolCounter > 0 && isGrounded)
            {
                dashCoolCounter -= Time.deltaTime;
            }
        }
    }

    private void checkCheat()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            canDoubleJump = !canDoubleJump;
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            canClimbing = !canClimbing;
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            canDash = !canDash;
        }

    }

    void Jump()
    {
        rb.AddForce(new Vector2(0, jumpHeight), ForceMode2D.Impulse);
        jumps--;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        StartClimb(col);
    }

    void OnTriggerStay2D(Collider2D col)
    {
        StartClimb(col);
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Ladder"))
        {
            if (isClimbing)
            {
                isClimbing = false;
                rb.gravityScale = defaultGravity;
                activeSpeed = DefaultmoveSpeed;
                Debug.Log("Stop Climb");
            }
        }
    }

    private void StartClimb(Collider2D col)
    {
        if (col.CompareTag("Ladder"))
        {
            if (canClimbing)
            {
                if (!isClimbing)
                {
                    if (Input.GetKeyDown(KeyCode.UpArrow))
                    {
                        isClimbing = true;
                        rb.velocity = Vector3.zero;
                        rb.gravityScale = 0f;
                        activeSpeed = DefaultmoveSpeed / 1.5f;
                        Debug.Log("Start Climb");
                    }
                }
            }
        }
    }

    void OnCollisionStay2D(Collision2D col)
    {
        if (col.collider.CompareTag("Solid"))
        {
            if (col.contacts.Length > 0)
            {
                ContactPoint2D contact = col.contacts[0];
                if (Vector2.Dot(contact.normal, Vector2.up) > 0.5)
                {
                    isGrounded = true;
                    anim.SetBool("isGrounded", isGrounded);
                    HowManyJumps();
                }
            }
        }
    }

    void HowManyJumps()
    {
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
        if (col.collider.CompareTag("Solid"))
        {
            if (jumps == 2 || jumps == 1)
            {
                jumps--;
            }

            isGrounded = false;
            anim.SetBool("isGrounded", isGrounded);
        }
    }

    void flip(float velo)
    {
        if (velo > 0.1f)
        {
            spriteR.flipX = false;
        }
        else if (velo < -0.1f)
        {
            spriteR.flipX = true;
        }
    }
}