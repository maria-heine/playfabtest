using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private Vector3 _movementVector;
    private float _horizontalAxis;

    public Action<Vector3, float> InputUpdate;
    public Action ShootPress;
    
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log("Pow!");
            ShootPress?.Invoke();
        }

        _movementVector = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            _movementVector = transform.forward;
        }
        
        if (Input.GetKey(KeyCode.S))
        {
            _movementVector = -transform.forward;
        }
        if (Input.GetKey(KeyCode.A))
        {
            _movementVector = -transform.right;
        }
        
        if (Input.GetKey(KeyCode.D))
        {
            _movementVector = transform.right;
        }

        _horizontalAxis = Input.GetAxis("Mouse X");

        InputUpdate.Invoke(_movementVector, _horizontalAxis);
    }
}
