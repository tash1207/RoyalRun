using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] float adjustChunkMoveSpeedAmount = -2f;

    const string hitTrigger = "Hit";

    float cooldownDuration = 1f;
    float hitCooldownTimer = 1f;

    LevelGenerator levelGenerator;

    void Start()
    {
        levelGenerator = FindFirstObjectByType<LevelGenerator>();
    }

    void Update()
    {
        if (hitCooldownTimer > 0)
        {
            hitCooldownTimer -= Time.deltaTime;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (hitCooldownTimer <= 0)
        {
            levelGenerator.ChangeChunkMoveSpeed(adjustChunkMoveSpeedAmount);
            animator.SetTrigger(hitTrigger);
            hitCooldownTimer = cooldownDuration;
        }
    }
}
