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
                chunk.Initialize(new Vector2Int(x, y), y * _worldSize.x + x);
                _world.AddChunk(chunk);
            }
        }

        GenerateFence(chunkSize);
    }

    private void GenerateFence(Vector3 chunkSize)
    {
        float worldWidth = _worldSize.x * chunkSize.x;
        float worldDepth = _worldSize.y * chunkSize.z;

        float fenceHeight = chunkSize.y * 2f;
        float fenceThickness = chunkSize.x * 0.2f;

        Vector3 horizontalScale = new Vector3(worldWidth + fenceThickness * 2f, fenceHeight, fenceThickness);
        Vector3 verticalScale = new Vector3(fenceThickness, fenceHeight, worldDepth + fenceThickness * 2f);

        float halfHeight = fenceHeight / 2f;

        CreateFence(new Vector3(worldWidth / 2f - chunkSize.x / 2f, halfHeight, -chunkSize.z / 2f - fenceThickness / 2f), horizontalScale, "SouthFence");
        CreateFence(new Vector3(worldWidth / 2f - chunkSize.x / 2f, halfHeight, worldDepth - chunkSize.z / 2f + fenceThickness / 2f), horizontalScale, "NorthFence");
        CreateFence(new Vector3(-chunkSize.x / 2f - fenceThickness / 2f, halfHeight, worldDepth / 2f - chunkSize.z / 2f), verticalScale, "WestFence");
        CreateFence(new Vector3(worldWidth - chunkSize.x / 2f + fenceThickness / 2f, halfHeight, worldDepth / 2f - chunkSize.z / 2f), verticalScale, "EastFence");
    }

    private void CreateFence(Vector3 position, Vector3 scale, string name)
    {
        GameObject fence = GameObject.CreatePrimitive(PrimitiveType.Cube);
        fence.name = name;
        fence.transform.parent = _world.transform;
        fence.transform.position = position;
        fence.transform.localScale = scale;
    }
}
