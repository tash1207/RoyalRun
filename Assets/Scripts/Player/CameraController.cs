using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] ParticleSystem speedUpParticleSystem;
    [SerializeField] float minFOV = 40f;
    [SerializeField] float maxFOV = 90f;
    [SerializeField] float zoomDuration = 0.5f;
    [SerializeField] float zoomSpeedModifier = 2f;

    CinemachineCamera cinemachineCamera;

    void Awake()
    {
        cinemachineCamera = GetComponent<CinemachineCamera>();
    }

    public void ChangeCameraFOV(float speedAmount)
    {
        StopAllCoroutines();
        StartCoroutine(ChangeFOVRoutine(speedAmount));

        if (speedAmount > 0)
        {
            speedUpParticleSystem.Play();
        }
    }

    IEnumerator ChangeFOVRoutine(float speedAmount)
    {
        float startFOV = cinemachineCamera.Lens.FieldOfView;
        float targetFOV = Mathf.Clamp(startFOV + (speedAmount * zoomSpeedModifier), minFOV, maxFOV);

        float elapsedTime = 0f;

        while (elapsedTime < zoomDuration)
        {
            elapsedTime += Time.deltaTime;
            cinemachineCamera.Lens.FieldOfView =
                Mathf.Lerp(startFOV, targetFOV, elapsedTime / zoomDuration);
            yield return null;
        }

        cinemachineCamera.Lens.FieldOfView = targetFOV;
    }
}
