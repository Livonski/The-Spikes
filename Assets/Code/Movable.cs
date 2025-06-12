using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movable : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _rotationSpeed;

    public void Move(Vector3 inputDirection)
    {
        Vector3 direction = transform.TransformDirection(inputDirection.normalized);
        Vector3 newPosition = transform.position + direction * _movementSpeed * Time.deltaTime;
        transform.position = newPosition;
    }

    public void Rotate(float inputDirection)
    {
        Vector3 rotationEulers = new Vector3(0, 1, 0) * inputDirection * _rotationSpeed;
        transform.Rotate(rotationEulers);
    }
}
