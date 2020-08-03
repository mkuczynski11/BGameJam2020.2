using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollectionEvent : MonoBehaviour
{
    public IEnums.COLLECTIBLE_TYPE collectibleType;
    public GameObject[] onLeverCollectionBreakeable;

    void OnTriggerEnter2D()
    {
        if(collectibleType == IEnums.COLLECTIBLE_TYPE.END)
        {
            Debug.Log("End");
        }
        else if(collectibleType == IEnums.COLLECTIBLE_TYPE.LEVER)
        {
            foreach(GameObject _object in onLeverCollectionBreakeable)
            {
                Destroy(_object);
            }
        }
        Destroy(gameObject);
    }
}
