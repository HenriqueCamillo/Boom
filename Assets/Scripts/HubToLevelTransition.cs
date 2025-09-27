using UnityEngine;

public class HubToLevelTransition : MonoBehaviour
{
    [SerializeField] CameraSwitcher cameraSwitcher;
    private bool hasTransitioned;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasTransitioned)
            return;

        if (collision.CompareTag("Player"))
            Transition();
    }

    private void Transition()
    {
        hasTransitioned = true;
        cameraSwitcher.ViewLevel();
    }
}
