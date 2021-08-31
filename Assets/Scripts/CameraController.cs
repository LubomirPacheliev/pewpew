using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform followTarget;
    public bool playerIsNotDead;

    // Update is called once per frame
    void Update()
    {
        if(playerIsNotDead) transform.position = new Vector3(followTarget.position.x, followTarget.position.y, transform.position.z);
    }
}
