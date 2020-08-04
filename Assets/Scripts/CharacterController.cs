using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class CharacterController : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 100f;
    [SerializeField] private float jumpForce = 400f;                          // Amount of force added when the player jumps.
    [Range(0, .3f)] [SerializeField] private float movementSmoothing = .05f;  // How much to smooth out the movement
    [SerializeField] private bool airControl = true;                         // Whether or not a player can steer while jumping;
    [SerializeField] private LayerMask whatIsGround;                          // A mask determining what is ground to the character
    [SerializeField] private Transform groundCheck;                           // A position marking where to check if the player is grounded.

    const float groundedRadius = 0.2f;
    bool isGrounded;
    Rigidbody2D rb;
    bool isFacingRight = true;
    Vector3 velocity = Vector3.zero;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        bool wasGrounded = isGrounded;
        isGrounded = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundedRadius, whatIsGround);
        for(int i =0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                isGrounded = true;
            }
        }
    }

    public void Move(float move, bool jump)
    {
        if(isGrounded || airControl)
        {
            //find target velocity
            Vector2 targetVelocity = new Vector2(move * playerSpeed, rb.velocity.y);
            //smooth velocity
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, movementSmoothing);

            if (move > 0 && !isFacingRight)
                Flip();
            else if (move < 0 && isFacingRight)
                Flip();
        }

        if(isGrounded && jump)
        {
            isGrounded = false;
            rb.AddForce(new Vector2(0f, jumpForce));
        }
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    public void setSpeed(float speed)   
    {
        playerSpeed = speed;
    }
}

