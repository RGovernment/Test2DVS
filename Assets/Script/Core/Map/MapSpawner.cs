using System.Collections.Generic;
using UnityEngine;

public class MapSpawner : MonoBehaviour
{
    [SerializeField] private Transform spawnParent;
    [SerializeField] private Transform playerPos;
    [SerializeField] private GameObject chunkPrefab;
    [SerializeField] private float chunkSize = 5f;
    [SerializeField] private int chunkCount;

    private Dictionary<Vector2Int, GameObject> activeChunk = new ();

    private void Update()
    {
        Vector2Int playerChunkPos = new
            (
                Mathf.RoundToInt(playerPos.position.x / chunkSize),
                Mathf.RoundToInt(playerPos.position.y / chunkSize)
            );

        for (int x = -chunkCount; x <= chunkCount; x++)
        {
            for (int y = -chunkCount; y <= chunkCount; y++)
            {

                Vector2Int coord = new (playerChunkPos.x + x, playerChunkPos.y + y);
                if (!activeChunk.ContainsKey(coord))
                    SpawnChunk(coord);
                
            }
        }

        RemoveFarChunk(playerChunkPos);
    }

    private void SpawnChunk(Vector2Int playerCoord)
    {
        Vector3 worldPos = new (playerCoord.x * chunkSize, playerCoord.y * chunkSize, 0);

        GameObject chunk = Instantiate(chunkPrefab, worldPos, Quaternion.identity, spawnParent);

        activeChunk.Add(playerCoord, chunk);
    }

    private void RemoveFarChunk(Vector2Int chunkCoord)
    {
        List<Vector2Int> removeList = new();

        foreach (var chunk in activeChunk)
        {
            int distance = Mathf.Max
                (
                    Mathf.Abs(chunk.Key.x - chunkCoord.x),
                    Mathf.Abs(chunk.Key.y - chunkCoord.y)
                );
            if (distance > chunkCount + 1)
            {
                removeList.Add(chunk.Key);
            }
        }

        foreach (Vector2Int coord in removeList)
        {
            Destroy(activeChunk[coord]);
            activeChunk.Remove(coord);
        }
    }


}
