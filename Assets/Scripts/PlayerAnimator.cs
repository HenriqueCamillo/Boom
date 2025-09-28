using UnityEngine;

public class PlayerAnimator : MonoBehaviour {


    private const string IS_WALKING = "IsWalking";
    private const string IS_GROUNDED = "IsGrounded";
    private const string IS_FALLING = "IsFalling";


    [SerializeField] private Player player;


    private Animator animator;
    private SpriteRenderer sprite;

    public bool IsFacingLeft => sprite.flipX;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (player.GetMovementVectorNormalized().x > float.Epsilon)
            sprite.flipX = false;
        else if (player.GetMovementVectorNormalized().x < -float.Epsilon)
            sprite.flipX = true;

        animator.SetBool(IS_WALKING, player.IsWalking());
        animator.SetBool(IS_GROUNDED, player.IsGrounded());
        animator.SetBool(IS_FALLING, player.IsFalling());
    }

}