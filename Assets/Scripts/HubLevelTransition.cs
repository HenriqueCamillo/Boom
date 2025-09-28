using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class HubLevelTransition : MonoBehaviour
{
    [SerializeField] UnityEvent goingToHubEvents;
    [SerializeField] UnityEvent goingToLevelEvents;
    [SerializeField] UnityEvent avoidSkillStackingCheat;


    public bool IsInHub = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            bool enteringHub = this.transform.position.x < collision.transform.position.x;
            (enteringHub ? goingToHubEvents : goingToLevelEvents)?.Invoke();
            IsInHub = enteringHub;

            if (enteringHub)
                avoidSkillStackingCheat?.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            bool enteringHub = this.transform.position.x > collision.transform.position.x;
            if (IsInHub == enteringHub)
                return;

            (enteringHub ? goingToHubEvents : goingToLevelEvents)?.Invoke();
            IsInHub = enteringHub;
        }


    }
}