using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public AudioManager audioMenago;
    public void Quit()
    {
        Quit();
    }

    public void Play()
    {
        audioMenago.Play("Menu_Select");
        SceneManager.LoadScene(1);
    }
}
