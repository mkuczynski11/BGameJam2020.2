using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Minecart : MonoBehaviour
{
    public IEnums.TYPE type = IEnums.TYPE.MINECART;
    public GameObject player;
    bool activable = false;
    bool active = false;
    BoxCollider2D boxCollider;
    Rigidbody2D rb;
    float playerDistanceY = 1.35f;
    float playerDistanceX = 0.5f;
    public GameObject gamemanager;
    GameManager gamemanagerS;

    public AudioManager audioMenago;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        playerDistanceX += boxCollider.size.x + player.GetComponent<BoxCollider2D>().size.x;
        gamemanagerS = gamemanager.GetComponent<GameManager>();
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
                    if (player.transform.localScale.x > 0 && (gameObject.transform.position.x - player.transform.position.x) > 0)
                        activable = true;
                    else if (player.transform.localScale.x < 0 && (player.transform.position.x - gameObject.transform.position.x) > 0)
                        activable = true;
                    else activable = false;
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
            active = true;
            player.GetComponent<PlayerMovement>().isAbleToJump(false);
            player.GetComponent<PlayerMovement>().setPlayerSpeed(10f);
            player.GetComponent<Animator>().SetBool("isPushing", true);
            player.GetComponent<Animator>().SetTrigger("Push");
            player.GetComponent<CharacterController>().frezzeFlip = true;
            audioMenago.Play("Cart_Rolling");
        }

        if (Input.GetKeyDown("e") && active && !activable)
        {
            active = false;
            player.GetComponent<PlayerMovement>().isAbleToJump(true);
            player.GetComponent<PlayerMovement>().setPlayerSpeed(20f);
            player.GetComponent<Animator>().SetBool("isPushing", false);
            player.GetComponent<CharacterController>().frezzeFlip = false;
            audioMenago.Stop("Cart_Rolling");
        }

        if(Mathf.Abs(rb.velocity.y) > 5f)
        {
            if(active)
            {
                active = false;
                player.GetComponent<PlayerMovement>().isAbleToJump(true);
                player.GetComponent<PlayerMovement>().setPlayerSpeed(20f);
                player.GetComponent<Animator>().SetBool("isPushing", false);
                player.GetComponent<CharacterController>().frezzeFlip = false;
                audioMenago.Stop("Cart_Rolling");
            }
        }

        if(active)
        {
            gamemanagerS.canChange = false;
            Vector2 vel = player.GetComponent<Rigidbody2D>().velocity;
            if (vel.x < 0) vel.x += 0.43f;
            if (vel.x > 0) vel.x -= 0.43f;
            rb.velocity = vel;
        }
        else
        {
            gamemanagerS.canChange = true;
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }
    }
}
