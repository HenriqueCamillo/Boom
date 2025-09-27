using UnityEngine;

public class SkillCollectable : MonoBehaviour
{
    [SerializeField] Skill skill;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            if (TryGetComponent(out SkillExecutor skillExecutor))
                skillExecutor.AddSkill(skill);
    }
}
