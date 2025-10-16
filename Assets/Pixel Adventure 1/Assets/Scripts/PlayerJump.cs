using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] private PlayerMovementState playerMovementState;

    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private SpriteRenderer spriteRenderer;

    [SerializeField] private float jumpForce = 6f;
    [SerializeField] private float doublejumpForce = 6f;
    [SerializeField] private Vector2 wallJumpForce = new Vector2(4f, 8f);
    [SerializeField] private float wallJumpMovementCooldown = 0.2f;

    private PlayerMovement playerMovement;

    private float playerHalfHeight;
    private float playerHalfWidth;
    private bool canDoubleJump;

    private void Start()
    {
        playerHalfHeight = spriteRenderer.bounds.extents.y;
        playerHalfWidth = spriteRenderer.bounds.extents.x;
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            CheckJumpType();
        }
    }

    private void CheckJumpType()
    {
        bool isGrounded = GetIsGrounded();

        if (isGrounded)
        {
            playerMovementState.SetMoveState(PlayerMovementState.MoveState.Jump);
            Jump(jumpForce);
        }
        else
        {
            int direction = GetWallJumDirection();

            if (direction == 0 && canDoubleJump && rigidBody.velocity.y <= 0.1f)
            {
                DoubleJump();
            }
            else if (direction != 0)
            {
                WallJump(direction);
            }
        }
    }

    private int GetWallJumDirection()
    {
        if (Physics2D.Raycast(transform.position, Vector2.right, playerHalfWidth + 0.1f, LayerMask.GetMask("Ground")))
        {
            return -1; // jump left
        }
        else if (Physics2D.Raycast(transform.position, Vector2.left, playerHalfWidth + 0.1f, LayerMask.GetMask("Ground")))
        {
            return 1; // jump right
        }
        else
        {
            return 0; // no wall
        }
    }

    private bool GetIsGrounded()
    {
        bool hit = Physics2D.Raycast(transform.position, Vector2.down, playerHalfHeight + 0.1f, LayerMask.GetMask("Ground"));
        if (hit)
        {
            canDoubleJump = true;
        }
        return hit;
    }

    private void DoubleJump()
    {
        rigidBody.velocity = Vector2.zero;
        rigidBody.angularVelocity = 0;
        Jump(doublejumpForce);
        canDoubleJump = false;
        playerMovementState.SetMoveState(PlayerMovementState.MoveState.Double_Jump);
    }

    private void WallJump(int direction)
    {
        Vector2 force = wallJumpForce;
        force.x *= direction;
        rigidBody.velocity = Vector2.zero;
        rigidBody.angularVelocity = 0;
        playerMovement.wallJumpCooldown = wallJumpMovementCooldown;
        rigidBody.AddForce(force, ForceMode2D.Impulse);
        playerMovementState.SetMoveState(PlayerMovementState.MoveState.Wall_Jump);
    }

    private void Jump(float force)
    {
        rigidBody.AddForce(Vector2.up * force, ForceMode2D.Impulse);
    }
}
