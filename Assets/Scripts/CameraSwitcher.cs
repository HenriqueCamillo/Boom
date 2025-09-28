using DG.Tweening;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Camera hubCamera;
    [SerializeField] Camera levelCamera;

    [Space(5)]

    [Header("Transition")]
    [SerializeField] float transitionDuration;
    [SerializeField] Ease ease;

    private void Awake()
    {
        SetCamera(hubCamera);
    }

    public void ViewHub()
    {
        TransitionTo(hubCamera);
    }

    public void ViewLevel()
    {
        TransitionTo(levelCamera);
    }

    private void TransitionTo(Camera camera)
    {
        Camera.main.transform.DOMove(camera.transform.position, transitionDuration).SetEase(ease);
        DOTween.To(() => Camera.main.orthographicSize, x => Camera.main.orthographicSize = x, camera.orthographicSize, transitionDuration).SetEase(ease);

    }

    private void SetCamera(Camera camera)
    {
        Camera.main.transform.position = camera.transform.position;
        Camera.main.orthographicSize = camera.orthographicSize;
    }

    public void SoftReset()
    {
        SetCamera(levelCamera);
    }
}
