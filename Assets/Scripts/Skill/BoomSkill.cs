using UnityEngine;

[CreateAssetMenu(fileName = "BoomSkill", menuName = "BoomSkill", order = 0)]
public class BoomSkill : Skill
{
    [SerializeField] private float boomForce;

    public override void Execute(Player player)
    {
        base.Execute(player);
        player.Rigidbody.AddForce(new Vector2(0f, boomForce), ForceMode2D.Impulse);
    }

    public override void EndExecution(Player player)
    {   
        base.EndExecution(player);
    }
}