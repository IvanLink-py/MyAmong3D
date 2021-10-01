using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class PlayerControll : NetworkBehaviour
{
    private Rigidbody _myRig;
    private Vector2 _mouseInput;
    private Vector3 _newCameraRot;
    private int _noCameraTimer;
    
    
    public float speed;
    
    public Transform myCamera;
    public bool hasCamera;
    public Vector2 sensitivity;

    // Start is called before the first frame update
    void Start()
    {
        _myRig = transform.GetComponent<Rigidbody>();
        _noCameraTimer = 10;
    }
    
    void FixedUpdate()
    {
        // if (!hasAuthority) return;
        PlayerMovement();
        if (!hasCamera) return;
        CameraUpdate();
    }

    void PlayerMovement()
    {
        _myRig.velocity = Vector3.up * _myRig.velocity.y + transform.forward * (Input.GetAxis("Vertical") * speed) +
                          transform.right * (Input.GetAxis("Horizontal") * speed);
    }

    void CameraUpdate()
    {
        if (_noCameraTimer == 0)
        {
            _mouseInput = (Vector2.right * (Input.GetAxis("Mouse X") * sensitivity.x) +
                           Vector2.down * (Input.GetAxis("Mouse Y") * sensitivity.y));

            transform.RotateAround(transform.position, Vector3.up, _mouseInput.x);
            myCamera.RotateAround(myCamera.position, myCamera.right, _mouseInput.y);

            _newCameraRot = myCamera.localEulerAngles;
            
            if (_newCameraRot.x > 85 && _newCameraRot.x < 180)
            {
                myCamera.localEulerAngles = new Vector3(85, 0, 0);
            }

            if (_newCameraRot.x < 275 && _newCameraRot.x > 180)
            {
                myCamera.localEulerAngles = new Vector3(275, 0, 0);
            }
        }
        else
        {
            _noCameraTimer--;
        }
    }
}