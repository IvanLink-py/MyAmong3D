using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotor : MonoBehaviour
{
    public float speed = 15.0f;
    public Vector3 up = Vector3.up;

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(transform.position, up, speed);
    }
}
