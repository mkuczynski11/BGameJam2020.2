using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public IEnums.TYPE type = IEnums.TYPE.LEVER;
    public GameObject[] toDestroy;
    bool activable = false;
    public Sprite leftSprite;
    public Sprite rightSprite;
    Sprite currentSprite;

    public AudioManager audioMenago;

    void Start()
    {
        currentSprite = leftSprite;
        GetComponent<SpriteRenderer>().sprite = currentSprite;
        audioMenago = FindObjectOfType<AudioManager>();
    }

    void Update()
    {
        if(activable && Input.GetKeyDown("e"))
        {
            if (GetComponent<SpriteRenderer>().sprite == leftSprite)
                GetComponent<SpriteRenderer>().sprite = rightSprite;
            else if (GetComponent<SpriteRenderer>().sprite == rightSprite)
                GetComponent<SpriteRenderer>().sprite = leftSprite;
            currentSprite = GetComponent<SpriteRenderer>().sprite;
            handleObjects();
            audioMenago.Play("Lever");
            Debug.Log("Lever pulled");
        }
    }

    void OnTriggerEnter2D()
    {
        Debug.Log("Lever");
        activable = true;
    }

    void OnTriggerExit2D()
    {
        activable = false;
    }

    void handleObjects()
    {
        foreach(GameObject gobject in toDestroy)
        {
            gobject.GetComponent<SpriteRenderer>().enabled = !gobject.GetComponent<SpriteRenderer>().enabled;
            gobject.GetComponent<BoxCollider2D>().enabled = !gobject.GetComponent<BoxCollider2D>().enabled;
        }
    }
}
