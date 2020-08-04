﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Minecart : MonoBehaviour
{
    public IEnums.TYPE type = IEnums.TYPE.MINECART;
    public GameObject player;
    public GameObject minecartInOtherWorld;
    public LayerMask playerMask;
    bool activable = false;
    bool active = false;
    BoxCollider2D boxCollider;
    Rigidbody2D rb;
    float playerDistanceY = 0.6f;
    float playerDistanceX = 0.5f;
    private Transform minecartParent;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        playerDistanceX += boxCollider.size.x + player.GetComponent<BoxCollider2D>().size.x;
        minecartParent = gameObject.transform.parent;
    }

    void Update()
    {
        if (!active)
        {
            float distanceY, distanceX;
            distanceY = Mathf.Abs(Vector2.Distance(new Vector2(0f, gameObject.transform.position.y), new Vector2(0f, player.transform.position.y)));
            if (!(distanceY > playerDistanceY))
            {
                distanceX = Mathf.Abs(Vector2.Distance(new Vector2(gameObject.transform.position.x, 0f), new Vector2(player.transform.position.x, 0f)));
                if (distanceX <= playerDistanceX)
                {
                    activable = true;
                }
                else
                {
                    activable = false;
                }
            }
            else activable = false;
        }
        else activable = false;

        if (Input.GetKeyDown("e") && activable && !active)
        {
            Debug.Log("pulling");
            active = true;
            player.GetComponent<PlayerMovement>().isAbleToJump(false);
            player.GetComponent<PlayerMovement>().setPlayerSpeed(5f);
        }

        if (Input.GetKeyDown("e") && active && !activable)
        {
            Debug.Log("pulling stopped");
            active = false;
            player.GetComponent<PlayerMovement>().isAbleToJump(true);
            player.GetComponent<PlayerMovement>().setPlayerSpeed(20f);
        }

        if(active)
        {
            Vector2 vel = player.GetComponent<Rigidbody2D>().   velocity;
            if (vel.x < 0) vel.x += 0.14f;
            if (vel.x > 0) vel.x -= 0.14f;
            rb.velocity = vel;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
        Debug.Log("PLayer:" + player.GetComponent<Rigidbody2D>().velocity + " Minecart" + rb.velocity);
    }
}