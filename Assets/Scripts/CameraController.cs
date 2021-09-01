using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform followTarget;
    public bool playerIsNotDead;
    public bool isShaking;

    private float time = 0f;

    // Update is called once per frame
    void Update()
    {
        if(playerIsNotDead)
        {
            if (!isShaking) 
            {
                transform.position = new Vector3(followTarget.position.x, followTarget.position.y, transform.position.z);
            } else {
                for (int i = 0; i < 4; i++) {
                    transform.position = new Vector3(followTarget.position.x + Random.Range(-0.2f, 0.2f), followTarget.position.y + Random.Range(-0.2f, 0.2f), transform.position.z);
                }
            }
            // TOFIX: this makes the shaking keep going longer the longer you play https://docs.unity3d.com/ScriptReference/Time-time.html
            if(time < Time.time) isShaking = false;
            //IDEA
            //float time = Time.time + shakedur
            //while (Time.time <= time) ScreenShake
        }
    }

    public void ScreenShake(float shakedur) {
        time += shakedur;
        isShaking = true;
    }
}
