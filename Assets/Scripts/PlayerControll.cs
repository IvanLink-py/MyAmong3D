using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    private Rigidbody _myRig;
    private Vector2 _mouseInput;
    private Vector3 _newCameraRot;
    private int _noCameraTimer;

    public bool isLock = true;
    public Transform spawnPoint;
    
    public float speed;
    
    public Transform myCamera;
    public Vector2 sensitivity;

    // Start is called before the first frame update
    void Start()
    {
        _myRig = transform.GetComponent<Rigidbody>();
        _noCameraTimer = 10;
    }

    public void UnlockMoving()
    {
        if (!isLock) return;
        isLock = false;
        transform.position = spawnPoint.position;
        transform.rotation = spawnPoint.rotation;
        _myRig.isKinematic = false;
    }
    
    void FixedUpdate()
    {
        if (isLock) return;
        
        PlayerMovement();
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