using UnityEditor.Animations;
using UnityEngine;

public class ActiveSkillsUI : MonoBehaviour
{
    public void AddSkill(Skill skill)
    {
        if (skill != null)
        {
            Instantiate(skill.UI_Prefab, this.transform);
        }
    }

    public void UseFirstSkill()
    {
        Destroy(transform.GetChild(0).gameObject);
    }

    public void UseLastSkill()
    {
        Destroy(transform.GetChild(transform.childCount - 1).gameObject);
    }

    public void ClearSkills()
    {
        foreach (Transform child in transform)
        {
            Destroy(child);
        }
    }
}
