using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    private Vector2Int _gridPosition;

    public void Initialize(Vector2Int position)
    {
        _gridPosition = position;
        gameObject.transform.name = "chunk " + position;
    }
}
