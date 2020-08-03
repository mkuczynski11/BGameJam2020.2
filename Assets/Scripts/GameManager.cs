using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA;

public class GameManager : MonoBehaviour
{
    public string viewChange = "r";
    public LayerMask collisionAble;
    private float collisionRadius = 2.5f;
    GameObject world1, world2;

    void Start()
    {
        world1 = GameObject.FindGameObjectWithTag("World1");
        world2 = GameObject.FindGameObjectWithTag("World2");
        world2.SetActive(false);
    }

    void Update()
    {
        if(Input.GetKeyDown(viewChange))
        {
            changeView();
        }
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
        if (world1.active == true)
        {
            worldActive = world1;
            worldInActive = world2;
            playerTagActive = "Player1";
            playerTagInActive = "Player2";
        }
        else if (world2.active == true)
        {
            worldActive = world2;
            worldInActive = world1;
            playerTagActive = "Player2";
            playerTagInActive = "Player1";
        }

        playerActive = GameObject.FindGameObjectWithTag(playerTagActive).transform;
        playerCollider = playerActive.GetComponent<BoxCollider2D>();

        //check for collisions
        worldInActive.SetActive(true);
        Collider2D[] colliders = Physics2D.OverlapBoxAll(playerActive.position, new Vector2(playerCollider.size.x, playerCollider.size.y), collisionAble);
        foreach (Collider2D collider in colliders)
        {
            if (collider.transform.tag != playerTagActive && collider.transform.tag != playerTagInActive)
            {
                Debug.Log("Cannot change the view. Go into safe zone");
                Debug.Log(collider.name);
                worldInActive.SetActive(false);
                change = false;
            }
        }

        //change view
        if (change)
        {
            Debug.Log("Changing view");
            worldActive.SetActive(false);
            playerInActive = GameObject.FindGameObjectWithTag(playerTagInActive).transform;
            playerInActive.position = playerActive.position;
        }
    }
}
