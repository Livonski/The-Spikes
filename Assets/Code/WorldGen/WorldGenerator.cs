using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    [SerializeField] private Vector2Int _worldSize;
    [SerializeField] private GameObject _chunkPrefab;
    [SerializeField] private World _world;
    
    private void Start()
    {
        GenerateWorld();
    }

    private void GenerateWorld()
    {
        Vector3 chunkSize = _chunkPrefab.GetComponent<MeshRenderer>().bounds.size;
        for (int y = 0; y < _worldSize.y; y++)
        {
            for (int x = 0; x < _worldSize.x; x++)
            {
                Vector3 chunkPosition = new Vector3(x * chunkSize.x, 0, y * chunkSize.z);
                GameObject chunkGO = Instantiate(_chunkPrefab, chunkPosition, Quaternion.identity);
                Chunk chunk = chunkGO.GetComponent<Chunk>();
                chunk.Initialize(new Vector2Int(x,y), y * _worldSize.x + x);
                _world.AddChunk(chunk);
            }
        }
    }
}
