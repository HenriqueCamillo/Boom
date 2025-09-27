using UnityEngine;

[CreateAssetMenu(fileName = "JumpSkill", menuName = "JumpSkill", order = 0)]
public class JumpSkill : Skill
{
    public override void Execute(Player player)
    {
        base.Execute(player);

        player.StartJump();
    }

    public override void EndExecution(Player player)
    {
        base.EndExecution(player);
        
        player.EndJump();
    }
}