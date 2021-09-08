using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    private Rigidbody _myRig;
    private Vector3 _stopForce;
    private Vector2 _mouseInput;
    private Vector3 _newCameraRot;
    private int _noCameraTimer;

    public float speed;

    public Transform camera;
    public Vector2 sensitivity;

    // Start is called before the first frame update
    void Start()
    {
        _myRig = transform.GetComponent<Rigidbody>();
        _noCameraTimer = 10;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
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
            camera.RotateAround(camera.position, camera.right, _mouseInput.y);
        }
        else
        {
            _noCameraTimer--;
        }
    }
}