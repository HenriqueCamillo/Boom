using UnityEngine;
using UnityEngine.Events;

public class PressureButton : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] UnityEvent onPressed;
    [SerializeField] UnityEvent onReleased;

    [SerializeField] bool pressOnly;
    private bool isPressed;

    private void Awake()
    {
        LevelManager.OnHardReset += Reset;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            Press();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (pressOnly)
            return;

        if (collision.CompareTag("Player"))
        {
            isPressed = false;
            animator.SetBool("Pressed", false);
            onReleased?.Invoke();
        }
    }

    private void Press()
    {
        if (isPressed)
            return;

        isPressed = true;
        animator.SetBool("Pressed", true);
        onPressed?.Invoke();
    }

    private void Release()
    {
        if (!isPressed)
            return;
        
        isPressed = false;
        animator.SetBool("Pressed", false);
        onReleased?.Invoke();
    }

    private void Reset()
    {
        Release();
    }
}
