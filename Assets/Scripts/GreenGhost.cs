using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenGhost : MonoBehaviour
{
    public Transform Patrol1, Patrol2;
    public float EnemySpeed = 12f;
    Animator anim;
    Rigidbody2D rb;
    bool isFacingRight = true;
    bool goingRight = true;
    Transform targetTransform;

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
        }
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
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
        rb.velocity = new Vector2(EnemySpeed * sign, 0f);
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
