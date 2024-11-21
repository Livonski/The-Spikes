using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    //[SerializeField] private GameObject[] _possibleFeatures;
    [SerializeField] private FeatureGenerationData[] _possibleFeatures;
    public Vector2Int _gridPosition {get; private set;}
    public int _ID {get; private set;}

    private Vector3 _chunkSize;

    private List<WorldFeature> _features = new List<WorldFeature>();


    public void Initialize(Vector2Int position, int ID)
    {
        _gridPosition = position;
        _ID = ID;
        _chunkSize = GetComponent<MeshRenderer>().bounds.size;
        gameObject.transform.name = "chunk " + position;
        GenetateFeatures();
    }

    private void GenetateFeatures()
    {
        if (_possibleFeatures.Length == 0)
        {
            Debug.LogError("Possible features list not set");
            return;
        }

        for(int i = 0; i < _possibleFeatures.Length; i++) 
        {
            FeatureGenerationData generationData = _possibleFeatures[i];

            WorldFeature newFeature = generationData.feature.GetComponent<WorldFeature>();
            FeatureData featureData = generationData.feature.GetComponent<FeatureData>();
            if (newFeature == null || featureData == null)
                continue;

            int numFeatures = Random.Range((float)generationData.amount.x, (float)generationData.amount.y) >= generationData.rarity ? 1 : 0;
            Debug.Log($"{numFeatures}");

            for (int j = 0; j < numFeatures; j++)
            {
                Vector3 chunkCenter = new Vector3(_gridPosition.x * _chunkSize.x, 0, _gridPosition.y * _chunkSize.z);
                GameObject newFeatureGO = Instantiate(generationData.feature, chunkCenter, Quaternion.identity);
                newFeature.OnInstantiate(featureData, newFeatureGO, transform, _ID * 1000 + i * 100 + j);
            }
        }
    }
}

[System.Serializable]
public struct FeatureGenerationData
{
    public GameObject feature;
    public Vector2Int amount;
    public float rarity;
}
