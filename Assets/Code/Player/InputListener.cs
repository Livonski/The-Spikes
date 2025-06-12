using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class InputListener : MonoBehaviour
{
    private Movable _movable;

    private bool _canMove;

    private void Start()
    {
        _movable = GetComponent<Movable>();
        if (_movable == null)
            Debug.LogError("Player object misses Movable class");
        _canMove = _movable != null;
    }

    void Update()
    {
        if ((Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) && _canMove)
        {
            Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            _movable.Move(direction);
        }

        if (Input.GetKey(KeyCode.E) && _canMove)
        {
            float direction = 1.0f;
            _movable.Rotate(direction);
        }

        if (Input.GetKey(KeyCode.Q) && _canMove)
        {
            float direction = -1.0f;
            _movable.Rotate(direction);
        }
    }
}
