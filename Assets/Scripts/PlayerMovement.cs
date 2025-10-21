using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 500;
    public float runSpeed = 800;
    public float moveInput = 0;
    public float jumpForce = 300;
    public bool isJump = false;
    public bool isRun = false;

    public Rigidbody2D rb;
    public SpriteRenderer sprite;
    public GroundChecker groundChecker;
    // start zeby podlinkowac obiekty do naszego kodu
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        moveInput = Input.GetAxis("Horizontal");


        if (Input.GetKeyDown(KeyCode.Space))
        { 
            isJump = true; 
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            isRun = true;
        }


    }

    //FixedUpdate zalezy od ilosci sekund (co 0.02 sek) a nie klatek
    private void FixedUpdate()
    {

        if (moveInput > 0)
        {
            sprite.flipX = false;
        }
        else if (moveInput < 0)
        {
            sprite.flipX = true;
        }

        rb.velocity = new Vector2(moveInput * moveSpeed * Time.deltaTime, rb.velocity.y);

        if (isRun)
        {
            rb.velocity = new Vector2(moveInput * runSpeed * Time.deltaTime, rb.velocity.y);
            isRun = false;
        }
        

        if (isJump && groundChecker.isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce);
            isJump = false;
        }
        
    }
}
