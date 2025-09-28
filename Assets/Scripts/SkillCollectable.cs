using UnityEngine;

public class SkillCollectable : MonoBehaviour
{
    [SerializeField] Skill skill;
    public bool IsStartupCollectable { get; set; }

    private void Awake()
    {
        LevelManager.OnSoftReset += SoftReset;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.TryGetComponent(out SkillExecutor skillExecutor))
            {
                skillExecutor.AddSkill(skill);
                this.gameObject.SetActive(false);
            }
        }
    }

    public void SoftReset()
    {
        if (IsStartupCollectable)
            return;
            
        this.gameObject.SetActive(true);
    }
}
