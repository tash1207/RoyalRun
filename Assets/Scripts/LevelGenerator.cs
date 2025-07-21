using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] GameObject chunkPrefab;
    [SerializeField] Transform chunkParent;
    [SerializeField] int startingChunksAmount = 12;
    [SerializeField] float chunkLength = 10f;

    void Start()
    {
        for (int i = 0; i < startingChunksAmount; i++)
        {
            Vector3 chunkPosition = transform.position + Vector3.forward * chunkLength * i;
            Instantiate(chunkPrefab, chunkPosition, Quaternion.identity, chunkParent);
        }
    }
}
