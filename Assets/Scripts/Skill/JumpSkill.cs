using UnityEngine;

[CreateAssetMenu(fileName = "JumpSkill", menuName = "JumpSkill", order = 0)]
public class JumpSkill : Skill
{
    [SerializeField] private float jumpForce;
    public override void Execute(Player player)
    {
        base.Execute(player);
        player.SetIsPressingJump(true);
        player.Rigidbody.linearVelocity = new Vector2(player.Rigidbody.linearVelocity.x, jumpForce);
    }

    public override void EndExecution(Player player)
    {
        base.EndExecution(player);
        player.SetIsPressingJump(false);
    }
}