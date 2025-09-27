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
    private float groundCheckSize = 0.2f;

    private bool pressingJump = false;
    private bool isGrounded;
    private bool canMove = true;

    public Rigidbody2D Rigidbody => rb;

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
        if(canMove)
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
        skillExecutor.EndSkillExecution();
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

    private void ApplyGravityModifiers()
    {
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
        return inputVector.normalized;
    }

    public void EnableMovement(bool enable)
    {
        canMove = enable;
    }
}