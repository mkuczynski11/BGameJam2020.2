using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    bool isMoving = true;
    float move;
    bool jump;
    bool canJump = true;
    CharacterController controller;
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }
    void Update()
    {
        if (GetComponent<Rigidbody2D>().velocity.y == 0)
            canJump = true;
        if (isMoving)
        {
            move = Input.GetAxisRaw("Horizontal");
            jump = Input.GetAxisRaw("Jump") != 0;
            if (!canJump) jump = false;
            controller.Move(move, jump);
        }
        else
        {
            GetComponent<Animator>().SetFloat("SpeedX", 0f);
        }
    }
    public void isAbleToMove(bool moving)
    {
        isMoving = moving;
    }

    public void isAbleToJump(bool jump)
    {
        canJump = jump;
    }

    public void setPlayerSpeed(float speed)
    {
        GetComponent<CharacterController>().setSpeed(speed);
    }
}
