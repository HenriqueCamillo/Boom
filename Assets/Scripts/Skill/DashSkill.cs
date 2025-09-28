using UnityEngine;

[CreateAssetMenu(fileName = "DashSkill", menuName = "DashSkill", order = 0)]
public class DashSkill : Skill
{
    [SerializeField] private float dashForce;

    public override void Execute(Player player)
    {
        base.Execute(player);
        player.IsDahsing = true;
        player.DisableGravity();
        player.Rigidbody.linearVelocity = new Vector2(dashForce * (player.IsFacingLeft ? -1 : 1), 0f);
    }

    public override void EndExecution(Player player)
    {   
        player.IsDahsing = false;
        player.EnableGravity();
        player.Rigidbody.linearVelocity = new Vector2(0f, 0f);
        base.EndExecution(player);
    }
}