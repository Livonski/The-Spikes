using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WorldFeature : MonoBehaviour 
{
    private FeatureData _data;
    private GameObject _gameObject;
    public void OnInstantiate(FeatureData data, GameObject gameObject, Transform chunkTransform, int featureIndex)
    {
        _data = data;
        _gameObject = gameObject;
        SetTransform(chunkTransform, featureIndex);
        SetOffset();
        SetRotation();
    }

    private void SetTransform(Transform chunkTransform, int featureIndex)
    {
        _gameObject.transform.parent = chunkTransform;
        _gameObject.transform.name = _data.GetName() + " " + featureIndex;
    }

    private void SetOffset()
    {
        float offsetX = Random.Range(_data.GetOffsetVariation(0).x, _data.GetOffsetVariation(0).y);
        float offsetY = Random.Range(_data.GetOffsetVariation(1).x, _data.GetOffsetVariation(1).y);
        float offsetZ = Random.Range(_data.GetOffsetVariation(2).x, _data.GetOffsetVariation(2).y);
        _gameObject.transform.position += _data.GetOffset() + new Vector3(offsetX, offsetY, offsetZ);
    }
    private void SetRotation()
    {
        float rotationX = Random.Range(_data.GetRotationVariation(0).x, _data.GetRotationVariation(0).y);
        float rotationY = Random.Range(_data.GetRotationVariation(1).x, _data.GetRotationVariation(1).y);
        float rotationZ = Random.Range(_data.GetRotationVariation(2).x, _data.GetRotationVariation(2).y);
        _gameObject.transform.Rotate(new Vector3(rotationX, rotationY, rotationZ));
    }
}
