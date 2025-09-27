using System.Collections.Generic;
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
    private ExecutionOrder executionOrder = ExecutionOrder.Queue;

    private Skill lastExecutedSkill;

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
                break;

            case ExecutionOrder.Queue:
            default:
                skill = skills.First.Value;
                skills.RemoveFirst();
                break;
        }

        return skill;
    }
    
    public void ClearSkills()
    {
        skills.Clear();
    }


    public void AddSkill(Skill skill)
    {
        skills.AddLast(skill);
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
}