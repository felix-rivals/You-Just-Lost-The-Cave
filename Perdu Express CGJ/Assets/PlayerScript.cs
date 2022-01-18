using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    public float speed;
    public float jumpHeight;

    private Rigidbody2D rb;
    private float moveInput;

    public bool isColliding;
    

    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate() {
        moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
    }

    void Update() {
        if (isColliding){
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.AddForce(new Vector2(0, jumpHeight), ForceMode2D.Impulse);
            }
        }
    }

    void OnTriggerExit(Collider col){
        if (col.name == "Hero")
        {
            isColliding = false;
        }
    }

    void OnTriggerEnter(Collider col){
        if (col.name == "Hero"){
            isColliding = true;
        }
        
    }
}