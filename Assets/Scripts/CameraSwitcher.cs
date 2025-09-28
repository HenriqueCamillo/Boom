using DG.Tweening;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Camera hubCamera;
    [SerializeField] Camera levelCamera;

    private Vector3 hubCameraPosition;
    private Vector3 levelCameraPosition;

    [Space(5)]

    [Header("Transition")]
    [SerializeField] float transitionDuration;
    [SerializeField] Ease ease;

    private void Awake()
    {
        hubCameraPosition = hubCamera.transform.position;
        levelCameraPosition = levelCamera.transform.position;

        Camera.main.transform.position = hubCameraPosition;

        LevelManager.OnHardReset += Reset;
    }

    public void ViewHub()
    {
        TransitionTo(hubCameraPosition);
    }

    public void ViewLevel()
    {
        TransitionTo(levelCameraPosition);
    }

    private void TransitionTo(Vector3 position)
    {
        Camera.main.transform.DOMove(position, transitionDuration).SetEase(ease);
    }

    private void Reset()
    {
        Camera.main.transform.position = hubCameraPosition;
    }
}
