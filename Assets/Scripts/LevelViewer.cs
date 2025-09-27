using UnityEngine;

public class LevelViewer : MonoBehaviour
{
    [SerializeField] CameraSwitcher cameraSwitcher;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            cameraSwitcher.ViewLevel();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            cameraSwitcher.ViewHub();
    }
}
