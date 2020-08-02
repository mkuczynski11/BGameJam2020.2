using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public string viewChange = "r";
    public LayerMask collisionAble;
    private float collisionRadius = 2.5f;

    void Update()
    {
        if(Input.GetKeyDown(viewChange))
        {
            bool change = true;
            //get player transform
            string playerTag = "";
            Transform player = gameObject.transform;
            GameObject world = gameObject;
            if (GameObject.FindGameObjectWithTag("World1").active == true)
            {
                world = GameObject.FindGameObjectWithTag("World1");
                playerTag = "Player1";
            }
            else if (GameObject.FindGameObjectWithTag("World2").active == true)
            {
                world = GameObject.FindGameObjectWithTag("World2");
                playerTag = "Player2";
            }

            for(int i =0; i < world.transform.childCount; i++)
            {
                if(world.transform.GetChild(i).tag == playerTag)
                {
                    player = world.transform.GetChild(i);
                }
            }
            //check for collisions
            Collider2D[] colliders = Physics2D.OverlapBoxAll(player.position, new Vector2(collisionRadius, collisionRadius), collisionAble);
            foreach(Collider2D collider in colliders)
            {
                if(collider.transform != player)
                {
                    Debug.Log("Cannot change the view. Go into safe zone");
                    change = false;
                }
            }

            //change view
            if(change)
            {
                Debug.Log("Changing view");
            }
        }
    }
}
