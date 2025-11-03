using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 500;
    public float runSpeed = 800;
    public float moveInput = 0;
    public float jumpForce = 300;

    public bool isJump = false;
    public bool isRun = false;

    public int maxJumps = 2;
    private int jumpCount = 0;

    public Rigidbody2D rb;
    public SpriteRenderer sprite;
    public GroundChecker groundChecker;
    public Animator anim;

    private bool canDoubleJump = true;

    // start zeby podlinkowac obiekty do naszego kodu
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        anim.SetFloat("VerticalVelocity", rb.velocity.y);
        anim.SetBool("IsGrounded", groundChecker.isGrounded);

        moveInput = Input.GetAxis("Horizontal");

        if (moveInput != 0)
        {
            anim.SetFloat("IsMove", 1);
        }
        else
        {
            anim.SetFloat("IsMove", -1);
        }

        if (isJump)
        {
            anim.SetBool("IsJump", true);
        }
        else { anim.SetBool("IsJump", false); }

       
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (groundChecker.isGrounded)
            {
                isJump = true;
                jumpCount = 1;
                canDoubleJump = true;
            }
            else if (canDoubleJump)
            {
                isJump = true;
                jumpCount = 2;
                canDoubleJump = false;
            }
        }

        if (!groundChecker.isGrounded)
        {
            if (jumpCount == 1)
            {
                anim.SetBool("IsJump", true);
                anim.SetBool("IsDoubleJump", false);
            }
            else if (jumpCount == 2)
            {
                anim.SetBool("IsJump", false);
                anim.SetBool("IsDoubleJump", true);
            }
        }
        else
        {
            // reset po l¹dowaniu
            jumpCount = 0;
            anim.SetBool("IsJump", false);
            anim.SetBool("IsDoubleJump", false);
            canDoubleJump = true;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            isRun = true;
        }

    }




    //FixedUpdate zalezy od ilosci sekund (co 0.02 sek) a nie klatek
    private void FixedUpdate()
    {
        if (groundChecker.isGrounded && !isJump && rb.velocity.y <= 0.1f)
        {
            jumpCount = 0;
        }

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


        if (isJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(Vector2.up * jumpForce); jumpCount++;
            isJump = false;

        }

       

    }
}
