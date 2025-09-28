using UnityEngine;
using UnityEngine.Events;

public class HubLevelTransition : MonoBehaviour
{
    [SerializeField] UnityEvent goingToHubEvents;
    [SerializeField] UnityEvent goingToLevelEvents;
    [SerializeField] UnityEvent avoidSkillStackingCheat;


    private bool inHub = true;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            bool enteringHub = this.transform.position.x < collision.transform.position.x;
            (enteringHub ? goingToHubEvents : goingToLevelEvents)?.Invoke();
            inHub = enteringHub;

            if (enteringHub)
                avoidSkillStackingCheat?.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            bool enteringHub = this.transform.position.x > collision.transform.position.x;
            if (inHub == enteringHub)
                return;

            (enteringHub ? goingToHubEvents : goingToLevelEvents)?.Invoke();
            inHub = enteringHub;
        }

        
    }
}