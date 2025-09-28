using UnityEngine;

public class SkillCollectable : MonoBehaviour
{
    [SerializeField] Skill skill;

    private void Awake()
    {
        LevelManager.OnHardReset += Reset;
        LevelManager.OnSoftReset += Reset;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            var skillExecutor = collision.GetComponentInParent<SkillExecutor>();
            if (skillExecutor != null)
            {
                skillExecutor.AddSkill(skill);
                this.gameObject.SetActive(false);
            }
        }
    }

    public void Reset()
    {
        this.gameObject.SetActive(true);
    }
}
