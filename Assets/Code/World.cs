using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    private List<Chunk> _world = new List<Chunk>();
    
    public void AddChunk(Chunk newChunk)
    {
        _world.Add(newChunk);
        newChunk.transform.parent = transform;
    }

    public Chunk GetChunk(int ID)
    {
        if( _world.Count == 0 )
        {
            Debug.LogError("Trying to acces chunk while world is empty");
            return null;
        }

        foreach (Chunk chunk in _world)
        {
            if (chunk._ID == ID)
                return chunk;
        }
        Debug.LogError($"No valid chunks with ID: {ID} found");
        return null;
    }

    public Chunk GetChunk(Vector2Int gridPosition)
    {
        if (_world.Count == 0)
        {
            Debug.LogError("Trying to acces chunk while world is empty");
            return null;
        }

        foreach (Chunk chunk in _world)
        {
            if (chunk._gridPosition == gridPosition)
                return chunk;
        }
        Debug.LogError($"No valid chunks in position: {gridPosition} found");
        return null;
    }
}
