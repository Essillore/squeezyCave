using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSpring : MonoBehaviour
{
    public float velocity = 0;
    public float force = 0;
    //current height
    public float height = 0f;
    //normal height
    private float target_height = 0f;
    // Start is called before the first frame update

    public void Update()
    {
    }


    public void WaveSpringUpdate(float springStiffness, float dampening)
    {
        height = transform.localPosition.y;
        //maximum extension
        var x = height - target_height;
        var loss = -dampening * velocity;


        force = -springStiffness * x + loss;
        velocity += force;
        var y = transform.localPosition.y;
        transform.localPosition = new Vector3(transform.localPosition.x, y+velocity, transform.localPosition.z);
    }


}
