using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovementState : MonoBehaviour
{
    public enum MoveState
    {
        Idle,
        Run,
        Jump,
        Fall,
        Double_Jump,
        Wall_Jump,
    }

    public MoveState CurrentMoveState { get; private set; }

    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D aRigidBody;

    private const string idleAnim = "Idle";
    private const string runAnim = "Run";
    private const string jumpAnim = "Jump";
    private const string fallAnim = "Fall";
    private const string doubleJumpAnim = "DoubleJump";
    private const string wallJumpAnim = "Wall Jump";

    public static Action<MoveState> OnPlayerMoveStateChanged;

    private float xPosLastFrame;

    private void Update()
    {
        // Idle
        if (transform.position.x == xPosLastFrame && aRigidBody.velocity.y == 0)
        {
            SetMoveState(MoveState.Idle);
        }
        // Running
        else if (transform.position.x != xPosLastFrame && aRigidBody.velocity.y == 0)
        {
            SetMoveState(MoveState.Run);
        }
        // Falling
        else if (aRigidBody.velocity.y < 0)
        {
            SetMoveState(MoveState.Fall);
        }

        xPosLastFrame = transform.position.x;
    }

    public void SetMoveState(MoveState moveState)
    {
        if (moveState == CurrentMoveState) return;

        switch (moveState)
        {
            case MoveState.Idle:
                HandleIdle();
                break;

            case MoveState.Run:
                HandleRun();
                break;

            case MoveState.Jump:
                HandleJump();
                break;

            case MoveState.Fall:
                HandleFall();
                break;

            case MoveState.Double_Jump:
                HandleDouble_Jump();
                break;

            case MoveState.Wall_Jump:
                HandleWall_Jump();
                break;

            default:
                break;
        }

        OnPlayerMoveStateChanged?.Invoke(moveState);
        CurrentMoveState = moveState;
    }

    private void HandleIdle()
    {
        animator.Play(idleAnim);
    }

    private void HandleRun()
    {
        animator.Play(runAnim);
    }

    private void HandleJump()
    {
        animator.Play(jumpAnim);
    }

    private void HandleFall()
    {
        animator.Play(fallAnim);
    }

    private void HandleDouble_Jump()
    {
        animator.Play(doubleJumpAnim);
    }

    private void HandleWall_Jump()
    {
        animator.Play(wallJumpAnim);
    }
}
