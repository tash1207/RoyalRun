using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [Header("References")]
    [SerializeField] CameraController cameraController;
    [SerializeField] GameObject chunkPrefab;
    [SerializeField] Transform chunkParent;
    [SerializeField] ScoreManager scoreManager;

    [Header("Settings")]
    [SerializeField] int startingChunksAmount = 12;
    [Tooltip("Do not change chunk length value unless chunk prefab size reflects change")]
    [SerializeField] float chunkLength = 10f;
    [SerializeField] float moveSpeed = 8f;
    [SerializeField] float minMoveSpeed = 4f;
    [SerializeField] float maxMoveSpeed = 26f;
    [SerializeField] float minGravityZ = -24f;
    [SerializeField] float maxGravityZ = -5f;

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
        float newMoveSpeed = moveSpeed + speedAmount;
        newMoveSpeed = Mathf.Clamp(newMoveSpeed, minMoveSpeed, maxMoveSpeed);

        if (newMoveSpeed != moveSpeed)
        {
            moveSpeed = newMoveSpeed;

            float newGravityZ = Physics.gravity.z - speedAmount;
            newGravityZ = Mathf.Clamp(newGravityZ, minGravityZ, maxGravityZ);

            Physics.gravity = new Vector3(Physics.gravity.x, Physics.gravity.y, newGravityZ);
            cameraController.ChangeCameraFOV(speedAmount);
        }
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
        GameObject newChunkGO = Instantiate(chunkPrefab, chunkPosition, Quaternion.identity, chunkParent);
        newChunkGO.GetComponent<Chunk>().Init(this, scoreManager);
        chunks.Add(newChunkGO);
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
