using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeatureData : MonoBehaviour
{
    [SerializeField] private string _name;
    [SerializeField] private Vector3 _baseOffset;

    [SerializeField] private Vector2 _offsetXVariation;
    [SerializeField] private Vector2 _offsetYVariation;
    [SerializeField] private Vector2 _offsetZVariation;

    [SerializeField] private Vector2 _rotationXVariation;
    [SerializeField] private Vector2 _rotationYVariation;
    [SerializeField] private Vector2 _rotationZVariation;

    public string GetName()
    {
        return _name; 
    }

    public Vector3 GetOffset()
    {
        return _baseOffset; 
    }

    public Vector2 GetOffsetVariation(int index)
    {
        switch (index)
        {
            case 0:
                return _offsetXVariation;
            case 1:
                return _offsetYVariation;
            case 2:
                return _offsetZVariation;
            default:
                Debug.LogError($"Invalid offset variation index, got: {index}, expected value in range 0 - 2");
                return Vector2.zero;
        }
    }

    public Vector2 GetRotationVariation(int index)
    {
        switch (index)
        {
            case 0:
                return _rotationXVariation;
            case 1:
                return _rotationYVariation;
            case 2:
                return _rotationZVariation;
            default:
                Debug.LogError($"Invalid rotation variation index, got: {index}, expected value in range 0 - 2");
                return Vector2.zero;
        }
    }
}
