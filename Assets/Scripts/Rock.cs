using Unity.Cinemachine;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField] ParticleSystem collisionParticleSystem;
    [SerializeField] AudioSource boulderSmashAudioSource;
    [SerializeField] float shakeModifier = 10f;

    CinemachineImpulseSource cinemachineImpulseSource;

    float collisionFXCooldownDuration = 0.6f;
    float collisionFXCooldownTimer = 0f;

    void Awake()
    {
        cinemachineImpulseSource = GetComponent<CinemachineImpulseSource>();
    }

    void Update()
    {
        if (collisionFXCooldownTimer > 0f)
        {
            collisionFXCooldownTimer -= Time.deltaTime;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (collisionFXCooldownTimer <= 0f)
        {
            FireImpluse();
            CollisionFX(other);
        }
    }

    void FireImpluse()
    {
        float distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        float shakeIntensity = shakeModifier / distance;
        shakeIntensity = Mathf.Min(shakeIntensity, 1f);
        cinemachineImpulseSource.GenerateImpulse(shakeIntensity);
    }

    void CollisionFX(Collision other)
    {
        ContactPoint contactPoint = other.GetContact(0);
        collisionParticleSystem.transform.position = contactPoint.point;
        collisionParticleSystem.Play();
        boulderSmashAudioSource.Play();
        collisionFXCooldownTimer = collisionFXCooldownDuration;
    }
}
