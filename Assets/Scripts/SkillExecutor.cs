using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkillExecutor : MonoBehaviour
{
    enum ExecutionOrder
    {
        Queue,
        Stack,
    }
    [SerializeField] Player player;
    private LinkedList<Skill> skills = new();
    private List<Skill> savedSkills;
    private ExecutionOrder executionOrder = ExecutionOrder.Queue;

    private Skill lastExecutedSkill;

    [SerializeField] ActiveSkillsUI skillsUI;

    private void Update()
    {
        if (lastExecutedSkill != null)
            lastExecutedSkill.UpdateExecution(player);
    }

    private Skill PopSkill()
    {
        if (skills.Count == 0)
            return null;

        Skill skill;
        switch (executionOrder)
        {
            case ExecutionOrder.Stack:
                skill = skills.Last.Value;
                skills.RemoveLast();
                skillsUI.UseLastSkill();
                break;

            case ExecutionOrder.Queue:
            default:
                skill = skills.First.Value;
                skills.RemoveFirst();
                skillsUI.UseFirstSkill();
                break;
        }

        return skill;
    }

    public void ClearSkills()
    {
        skills.Clear();
        skillsUI.ClearSkills();
    }


    public void AddSkill(Skill skill)
    {
        skills.AddLast(skill);
        skillsUI.AddSkill(skill);
    }

    public void ExecuteNextSkill()
    {
        Skill skill = PopSkill();
        if (skill == null)
            return;

        ExecuteSkill(skill);
    }

    private void ExecuteSkill(Skill skill)
    {
        lastExecutedSkill = skill;
        skill.Execute(player);
    }

    public void EndSkillExecution()
    {
        if (lastExecutedSkill == null)
            return;

        if (!lastExecutedSkill.IsExecuting)
            return;

        lastExecutedSkill.EndExecution(player);
    }

    public void SetSavedSkills()
    {
        Skill[] tempArray = new Skill[skills.Count];
        skills.CopyTo(tempArray, 0);
        savedSkills = tempArray.ToList<Skill>();
    }

    public void ResetSkills()
    {
        ClearSkills();
        foreach (Skill skill in savedSkills)
        {
            AddSkill(skill);
        }
    }
}