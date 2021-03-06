﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]

public class CharacterController : MonoBehaviour
{
    public Animator anim;
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
    public bool frezzeFlip = false;
    Vector3 velocity = Vector3.zero;

    public ParticleSystem dust;
    public ParticleSystem blood;
    public ParticleSystem rewindParticles;

    public AudioManager audioMenago;
    public BoxCollider2D bCollider;
    public CircleCollider2D circleCollider1;
    public CircleCollider2D circleCollider2;
    public GameObject gameManagerObject;
    GameManager gameManager;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        gameManager = gameManagerObject.GetComponent<GameManager>();
        audioMenago = FindObjectOfType<AudioManager>();
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
        anim.SetBool("isGrounded", isGrounded);
    }

    public void Move(float move, bool jump)
    {
        if(isGrounded || airControl)
        {
            //find target velocity
            Vector2 targetVelocity = new Vector2(move * playerSpeed, rb.velocity.y);
            //smooth velocity
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, movementSmoothing);
            anim.SetFloat("SpeedX", Mathf.Abs(rb.velocity.x));

            if (move > 0 && !isFacingRight)
                Flip();
            else if (move < 0 && isFacingRight)
                Flip();
        }

        if (rb.velocity.y <= 0f && rb.velocity.y >= -0.2f)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0f);
        }

        if(isGrounded && jump && rb.velocity.y == 0f)
        {
            PlayDust();
            audioMenago.Play("jump_quotes");
            isGrounded = false;
            rb.AddForce(new Vector2(0f, jumpForce));
        }
        anim.SetFloat("SpeedY", rb.velocity.y);
    }

    void Flip()
    {
        if(isGrounded)
        {
            PlayDust();
        }
        if (!frezzeFlip)
        {
            isFacingRight = !isFacingRight;

            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }


    public void setSpeed(float speed)   
    {
        playerSpeed = speed;
    }

    public void disableColliders()
    {
        bCollider.enabled = false;
        circleCollider1.enabled = false;
        circleCollider2.enabled = false;
    }

    public void enableColliders()
    {
        bCollider.enabled = true;
        circleCollider1.enabled = true;
        circleCollider2.enabled = true;
    }

    public void DamagePlayer(float damage)
    {
        gameManager.playerHp -= damage;
        anim.SetTrigger("Hit");
        PlayBlood();
        audioMenago.Play("hurt_quotes");
        checkDeath();
    }

    void checkDeath()
    {
        if (gameManager.playerHp <= 0f)
        {
            anim.SetTrigger("Dead");
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            gameObject.GetComponent<PlayerMovement>().isAbleToMove(false);
            gameObject.tag = "Dead";
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, -10f);
            gameManager.restartButton.active = true;
        }
    }

    void PlayDust()
    {
        dust.Play();
    }

    void PlayBlood()
    {
        blood.Play();
    }

    void PlayRewindParticles()
    {
        rewindParticles.Play();
    }
}

