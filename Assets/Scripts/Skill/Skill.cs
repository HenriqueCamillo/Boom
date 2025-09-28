using System;
using UnityEngine;

public abstract class Skill : ScriptableObject
{
    protected bool isExecuting;
    public bool IsExecuting => isExecuting;
    [SerializeField] public float duration = 0;
    [SerializeField] public bool EndOnButtonRelease;
    [SerializeField] public bool DisableMovement;

    private float initialTime;

    public virtual void Execute(Player player)
    {
        isExecuting = true;
        initialTime = Time.time;
        if (DisableMovement)
        {
            Debug.Log("disable movement");
            player.EnableMovement(false);
        }
    }

    public virtual void EndExecution(Player player)
    {
        isExecuting = false;
        if (DisableMovement)
        {
            Debug.Log("enable movement back");
            player.EnableMovement(true);
        }
    }

    public virtual void UpdateExecution(Player player)
    {
        if (isExecuting && duration > 0)
        {
            if (Time.time > initialTime + duration)
                EndExecution(player);
        }
    }
}