using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    [SerializeField] private bool infiniteJumps;

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float fallGravityScale = 2.25f;
    [SerializeField] private float lowJumpGravityScale = 1f;
    [SerializeField] private float normalGravityScale = 1.75f;

    private PlayerInputActions playerInputActions;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private SkillExecutor skillExecutor;
    [SerializeField] private PlayerAnimator animator;
    private float groundCheckSize = 0.2f;

    private bool pressingJump = false;
    private bool isGrounded;
    private bool canMove = true;
    private bool gravityDisabled = false;
    public bool IsDahsing { get; set; }

    public Rigidbody2D Rigidbody => rb;
    public bool IsFacingLeft => animator.IsFacingLeft;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one Player instance");
            return;
        }
        Instance = this;
        playerInputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        playerInputActions.Player.Enable();

        playerInputActions.Player.Action.performed += ExecuteSkill;
        playerInputActions.Player.Action.canceled += EndSkillExecution;

        playerInputActions.Player.Jump.performed += Jump;
        playerInputActions.Player.Jump.canceled += Jump;
    }

    private void OnDisable()
    {
        playerInputActions.Player.Action.performed -= ExecuteSkill;
        playerInputActions.Player.Action.canceled -= EndSkillExecution;

        playerInputActions.Player.Jump.performed -= Jump;
        playerInputActions.Player.Jump.canceled -= Jump;
    }

    private void FixedUpdate()
    {
        ApplyGravityModifiers();
    }

    private void Update()
    {
        if (canMove)
            HandleMovement();
    }

    private void HandleMovement()
    {
        Vector2 inputVector = GetMovementVectorNormalized();
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckSize, groundLayer);
        rb.linearVelocity = new Vector2(inputVector.x * moveSpeed, rb.linearVelocity.y);
    }

    private void ExecuteSkill(InputAction.CallbackContext ctx) => ExecuteSkill();
    private void ExecuteSkill()
    {
        skillExecutor.ExecuteNextSkill();
    }

    private void EndSkillExecution() => EndSkillExecution();
    private void EndSkillExecution(InputAction.CallbackContext ctx)
    {
        skillExecutor.TryEndExecutionOnButtonRelease();
    }

    public void Jump(InputAction.CallbackContext ctx)
    {
        if (!infiniteJumps)
            return;

        if (ctx.performed)
            StartJump();
        else if (ctx.canceled)
            EndJump();
    }

    public void StartJump()
    {
        pressingJump = true;
        if (isGrounded)
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }

    public void EndJump()
    {
        pressingJump = false;
    }

    public void DisableGravity()
    {
        gravityDisabled = true;
        rb.gravityScale = 0f;
    }

    public void EnableGravity()
    {
        gravityDisabled = false;
        rb.gravityScale = 1f;
    }

    private void ApplyGravityModifiers()
    {
        if (gravityDisabled)
            return;

        if (rb.linearVelocity.y < 0f)
            rb.gravityScale = fallGravityScale;
        else if (pressingJump && rb.linearVelocity.y > 0f)
            rb.gravityScale = lowJumpGravityScale;
        else
            rb.gravityScale = normalGravityScale;
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
        var horizontalOnlyInput = new Vector2(inputVector.x, 0f);
        return horizontalOnlyInput.normalized;
    }

    public void EnableMovement(bool enable)
    {
        canMove = enable;
        animator.LockFlip = !canMove;
    }

    public void SetIsPressingJump(bool isPressingJump)
    {
        pressingJump = isPressingJump;
    }

    public bool IsWalking()
    {
        return Mathf.Abs(rb.linearVelocity.x) > 0.01f;
    }
    public bool IsGrounded()
    {
        return isGrounded;
    }
    public bool IsFalling()
    {
        return rb.linearVelocity.y < 0.01f;
    }

    public void Reset()
    {
        skillExecutor.EndSkillExecution();
        skillExecutor.ResetSkills(); 
        rb.linearVelocity = Vector2.zero;
    }
}