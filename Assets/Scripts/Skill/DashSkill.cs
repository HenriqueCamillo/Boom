using UnityEngine;

[CreateAssetMenu(fileName = "DashSkill", menuName = "DashSkill", order = 0)]
public class DashSkill : Skill
{
    [SerializeField] private float dashForce;

    public override void Execute(Player player)
    {
        base.Execute(player);
        var playerMovement = player.GetMovementVectorNormalized();
        if (playerMovement.x > 0f)
        {
            player.Rigidbody.linearVelocity = new Vector2(dashForce, 0f);
        }
        else
        {
            player.Rigidbody.linearVelocity = new Vector2(-dashForce, 0f);
        }
    }

    public override void EndExecution(Player player)
    {   
        player.Rigidbody.linearVelocity = new Vector2(0f, 0f);
        base.EndExecution(player);
    }
}