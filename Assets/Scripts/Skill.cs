using UnityEngine;

public abstract class Skill : ScriptableObject
{
    protected bool isExecuting;
    public bool IsExecuting => isExecuting;

    public virtual void Execute(Player player)
    {
        isExecuting = true;
    }

    public virtual void EndExecution(Player player) 
    {
        isExecuting = false;
    }
}