using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform cameraFollow;
    void Update()
    {
        transform.position = cameraFollow.position + new Vector3(0f,0f,-10f);
    }

    public void followChange(Transform newFollow)
    {
        cameraFollow = newFollow;
    }
}
