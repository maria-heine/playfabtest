using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputManager))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 10f;
    [SerializeField] private float _rotationSpeed = 10f;

    private InputManager _inputManager;

    private void Awake()
    {
        _inputManager = GetComponent<InputManager>();

        _inputManager.InputUpdate += MovePlayer;
    }

    private void MovePlayer(Vector3 direction, float rotation)
    {
        transform.position += direction * Time.deltaTime * _movementSpeed;

        transform.Rotate(0f, rotation * _rotationSpeed, 0f);
    }
}
