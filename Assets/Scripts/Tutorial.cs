using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public Canvas canvas;

    void Start()
    {
        canvas.enabled = false;
    }
    void OnTriggerEnter2D()
    {
        canvas.enabled = true;
    }

    void OnTriggerExit2D()
    {
        canvas.enabled = false;
    }
}
