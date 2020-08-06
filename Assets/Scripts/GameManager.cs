using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.WSA;

public class GameManager : MonoBehaviour
{
    public string viewChange = "r";
    public bool canChange = true;
    public LayerMask collisionAble;
    public GameObject mainCamera;
    GameObject world1, world2;
    GameObject activeWorld;
    Vector3 worldOffset = new Vector3(0f, 300f, 0f);
    public float playerHp = 100f;
    public Slider slider;

    void Start()
    {
        world1 = GameObject.FindGameObjectWithTag("World1");
        world2 = GameObject.FindGameObjectWithTag("World2");
        activeWorld = world1;
        GameObject.FindGameObjectWithTag("Player2").GetComponent<PlayerMovement>().isAbleToMove(false);
        GameObject.FindGameObjectWithTag("Player2").GetComponent<CharacterController>().disableColliders();
        GameObject.FindGameObjectWithTag("Player2").GetComponent<Rigidbody2D>().isKinematic = true;
    }

    void Update()
    {
        if(Input.GetKeyDown(viewChange) && canChange)
        {
            changeView();
        }
        slider.value = playerHp;
    }

    void changeView()
    {
        bool change = true;
        //get player transform
        string playerTagActive = "";
        string playerTagInActive = "";
        Transform playerActive = gameObject.transform;
        Transform playerInActive = gameObject.transform;
        BoxCollider2D playerCollider = null;
        GameObject worldActive = gameObject;
        GameObject worldInActive = gameObject;
        Vector3 offset = Vector3.zero;
        if (world1 == activeWorld)
        {
            worldActive = world1;
            worldInActive = world2;
            playerTagActive = "Player1";
            playerTagInActive = "Player2";
            offset = -worldOffset;
        }
        else if (world2 == activeWorld)
        {
            worldActive = world2;
            worldInActive = world1;
            playerTagActive = "Player2";
            playerTagInActive = "Player1";
            offset = worldOffset;
        }

        playerActive = GameObject.FindGameObjectWithTag(playerTagActive).transform;
        playerCollider = playerActive.GetComponent<BoxCollider2D>();

        //check for collisions
        Collider2D[] colliders = Physics2D.OverlapBoxAll(playerActive.position + offset, new Vector2(playerCollider.size.x, playerCollider.size.y), collisionAble);
        foreach (Collider2D collider in colliders)
        {
            if (collider.transform.tag == "Floor" || collider.transform.tag == "Obsticle" || collider.transform.tag == "Minecart")
            {
                Debug.Log("Cannot change the view. Go into safe zone");
                Debug.Log(collider.name);
                change = false;
            }
        }

        //change view
        if (change)
        {
            Debug.Log("Changing view");
            activeWorld = worldInActive;
            playerInActive = GameObject.FindGameObjectWithTag(playerTagInActive).transform;
            playerInActive.GetComponent<CharacterController>().enableColliders();
            playerInActive.GetComponent<Rigidbody2D>().isKinematic = false;
            playerInActive.position = playerActive.position + offset;
            playerInActive.GetComponent<PlayerMovement>().isAbleToMove(true);
            playerActive.GetComponent<PlayerMovement>().isAbleToMove(false);
            playerInActive.GetComponent<Rigidbody2D>().velocity = playerActive.GetComponent<Rigidbody2D>().velocity;
            playerActive.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
            playerActive.GetComponent<CharacterController>().disableColliders();
            playerActive.GetComponent<Rigidbody2D>().isKinematic = true;
            playerActive.position = new Vector3(-1000f, -1000f, 0f);
            mainCamera.GetComponent<Camera>().followChange(playerInActive);
        }
    }
}
