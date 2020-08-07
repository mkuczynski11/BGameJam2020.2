using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GreenGhost : MonoBehaviour
{
    public Transform Patrol1, Patrol2;
    public float EnemySpeed = 12f;
    public LayerMask obsticleMask;
    Animator anim;
    Rigidbody2D rb;
    BoxCollider2D bCollider;
    bool isPlayerActive = true;
    bool isFacingRight = true;
    bool goingRight = true;
    Transform targetTransform;
    string playerTag1 = "Player1";
    string playerTag2 = "Player2";
    string minecartTag = "Minecart";
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public float attackRate = 1f;
    public float attackDamage = 40f;
    float nextAttackTime = 0f;
    GameObject player;

    void Start()
    {
        anim = GetComponent<Animator>();
        if (Mathf.Abs(Vector2.Distance(transform.position, Patrol1.position)) > Mathf.Abs(Vector2.Distance(transform.position, Patrol2.position)))
        {
            targetTransform = Patrol2;
            isFacingRight = true;
            goingRight = true;
        }
        else
        {
            targetTransform = Patrol1;
            isFacingRight = false;
            goingRight = false;
            Flip();
        }
        rb = GetComponent<Rigidbody2D>();
        bCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        bool isDead = false;
        Collider2D[] colliders = Physics2D.OverlapAreaAll(Patrol1.position, Patrol2.position, obsticleMask);
        foreach (Collider2D collider in colliders)
        {
            if ((collider.tag == "Player1" || collider.tag == "Player2"))
            {
                targetTransform = collider.transform;
            }
        }

        if (targetTransform.tag == "Dead") isDead = true;

        if(targetTransform != Patrol1 && targetTransform != Patrol2)
        {
            if (targetTransform.position.x <= Patrol1.position.x || targetTransform.position.x >= Patrol2.position.x || isDead)
            {
                if (Mathf.Abs(Vector2.Distance(transform.position, Patrol1.position)) > Mathf.Abs(Vector2.Distance(transform.position, Patrol2.position)))
                {
                    targetTransform = Patrol2;
                    if (!goingRight) Flip();
                    goingRight = isFacingRight = true;
                }
                else
                {
                    targetTransform = Patrol1;
                    if (goingRight) Flip();
                    goingRight = isFacingRight = false;
                }
            }
            else if(Time.time >= nextAttackTime)
            {
                Collider2D[] _colliders = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, obsticleMask);
                foreach(Collider2D collider in _colliders)
                {
                    bCollider.isTrigger = true;
                    Attack();
                    player = collider.gameObject;
                    nextAttackTime = Time.time + 1f / attackRate;
                    break;
                }
            }
        }

        if (goingRight && transform.position.x >= targetTransform.position.x)
        {
            isFacingRight = !isFacingRight;
            goingRight = !goingRight;
            if (targetTransform == Patrol1) targetTransform = Patrol2;
            else if (targetTransform == Patrol2) targetTransform = Patrol1;
            Flip();
        }
        else if (!goingRight && transform.position.x <= targetTransform.position.x)
        {
            isFacingRight = !isFacingRight;
            goingRight = !goingRight;
            if (targetTransform == Patrol1) targetTransform = Patrol2;
            else if (targetTransform == Patrol2) targetTransform = Patrol1;
            Flip();
        }

        float sign = 0f;
        if (goingRight) sign = 1f;
        else if(!goingRight) sign = -1f;
        if (Time.time >= nextAttackTime)
        {
            rb.velocity = new Vector2(EnemySpeed * sign, 0f);
            bCollider.isTrigger = false;
        }
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    void Attack()
    {
        anim.SetTrigger("Attack");
        rb.velocity = new Vector2(0f, 0f);
    }
    
    void DamagePlayer()
    {
        player.GetComponent<CharacterController>().DamagePlayer(attackDamage);
    }
}
