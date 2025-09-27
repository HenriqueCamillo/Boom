using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class Player : MonoBehaviour {
    public static Player Instance { get; private set; }


    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float fallGravityScale = 2.25f;
    [SerializeField] private float lowJumpGravityScale = 1f;
    [SerializeField] private float normalGravityScale = 1.75f;

    private PlayerInputActions playerInputActions;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    private float groundCheckSize = 0.2f;

    private bool pressingJump = false;
    private bool isGrounded;
    private bool isWalking;


    private void Awake() {
        if (Instance != null) {
            Debug.LogError("There is more than one Player instance");
        }
        Instance = this;
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
    }

    private void FixedUpdate()
    {
        ApplyGravityModifiers();  
    } 

    private void Update()
    {
        HandleMovement();
    }


    private void HandleMovement()
    {
        Vector2 inputVector = GetMovementVectorNormalized();
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckSize, groundLayer);

        rb.linearVelocity = new Vector2(inputVector.x * moveSpeed, rb.linearVelocity.y);

        playerInputActions.Player.Jump.performed += Jump;
        playerInputActions.Player.Jump.canceled += Jump;
    }

    void Jump(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            pressingJump = true;
            if (isGrounded)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            }
        }
        else if (ctx.canceled)
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

    public bool IsWalking()
    {
        return isWalking;
    }

    public Vector2 GetMovementVectorNormalized() {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
        return inputVector.normalized;
    }

}