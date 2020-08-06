using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Doors : MonoBehaviour
{
    bool active = false;

    void OnTriggerEnter2D()
    {
        active = true;
    }

    void OnTriggerExit2D()
    {
        active = false;
    }
    void Update()
    {
        if(Input.GetKeyDown("e") && active)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
