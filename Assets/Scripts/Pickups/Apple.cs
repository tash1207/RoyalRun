using UnityEngine;

public class Apple : Pickup
{
    [SerializeField] float adjustChunkMoveSpeedAmount = 3f;

    LevelGenerator levelGenerator;

    void Start()
    {
        levelGenerator = FindFirstObjectByType<LevelGenerator>();
    }

    protected override void OnPickup()
    {
        levelGenerator.ChangeChunkMoveSpeed(adjustChunkMoveSpeedAmount);
    }
}
