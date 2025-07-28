using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] GameObject chunkPrefab;
    [SerializeField] Transform chunkParent;
    [SerializeField] int startingChunksAmount = 12;
    [SerializeField] float chunkLength = 10f;
    [SerializeField] float moveSpeed = 8f;
    [SerializeField] float minMoveSpeed = 4f;
    [SerializeField] float maxMoveSpeed = 26f;

    List<GameObject> chunks = new List<GameObject>();

    void Start()
    {
        SpawnInitialChunks();
    }

    void Update()
    {
        MoveChunks();
    }

    public void ChangeChunkMoveSpeed(float speedAmount)
    {
        moveSpeed += speedAmount;

        if (moveSpeed < minMoveSpeed)
        {
            moveSpeed = minMoveSpeed;
        }
        else if (moveSpeed > maxMoveSpeed)
        {
            moveSpeed = maxMoveSpeed;
        }

        Physics.gravity = new Vector3(Physics.gravity.x, Physics.gravity.y, Physics.gravity.z - speedAmount);
    }

    void SpawnInitialChunks()
    {
        for (int i = 0; i < startingChunksAmount; i++)
        {
            SpawnChunk();
        }
    }

    void SpawnChunk()
    {
        Vector3 chunkPosition = CalculateChunkPosition();
        GameObject newChunk = Instantiate(chunkPrefab, chunkPosition, Quaternion.identity, chunkParent);

        chunks.Add(newChunk);
    }

    Vector3 CalculateChunkPosition()
    {
        if (chunks.Count == 0)
        {
            return transform.position;
        }
        else
        {
            return chunks[chunks.Count - 1].transform.position + Vector3.forward * chunkLength;
        }
    }

    void MoveChunks()
    {
        for (int i = 0; i < chunks.Count; i++)
        {
            GameObject chunk = chunks[i];
            chunk.transform.Translate(-transform.forward * (moveSpeed * Time.deltaTime));

            if (chunk.transform.position.z <= Camera.main.transform.position.z - chunkLength)
            {
                chunks.Remove(chunk);
                Destroy(chunk);
                SpawnChunk();
            }
        }
    }
}
