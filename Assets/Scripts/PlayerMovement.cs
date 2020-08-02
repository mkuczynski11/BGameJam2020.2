using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    float move;
    bool jump;
    CharacterController controller;
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }
    void Update()
    {
        move = Input.GetAxisRaw("Horizontal");
        jump = Input.GetAxisRaw("Jump") != 0;
        controller.Move(move, jump);
    }
}
