using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    [SerializeField] Animator animator;

    const string hitTrigger = "Hit";

    float cooldownDuration = 1f;
    float hitCooldownTimer = 1f;

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
            animator.SetTrigger(hitTrigger);
            hitCooldownTimer = cooldownDuration;
        }
    }
}
