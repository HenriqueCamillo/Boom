using UnityEngine;
using UnityEngine.Events;

public class PressureButton : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] UnityEvent onPressed;
    [SerializeField] UnityEvent onReleased;

    [SerializeField] bool pressOnly;
    private bool isPressed;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isPressed)
            return;

        if (collision.CompareTag("Player"))
        {
            isPressed = true;
            animator.SetBool("Pressed", true);
            onPressed?.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (pressOnly || !isPressed)
            return;

        if (collision.CompareTag("Player"))
        {
            isPressed = false;
            animator.SetBool("Pressed", false);
            onReleased?.Invoke();
        }
    }
}
