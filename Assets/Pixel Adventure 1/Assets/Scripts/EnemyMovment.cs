using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float speed = 3f;
    [SerializeField] private int startDirection = 1;

    private int CurrentDirection;
    private float halfWidth;
    private float halfHeight;
    private Vector2 movement;
    

    private void Start()
    {
        halfWidth = spriteRenderer.bounds.extents.x;
        halfHeight = spriteRenderer.bounds.extents.y;
        CurrentDirection = startDirection;
        spriteRenderer.flipX = startDirection == 1 ? false : true;
    }

    private void FixedUpdate()
    {
        movement.x = CurrentDirection * speed;
        movement.y = rigidBody.velocity.y;
        rigidBody.velocity = movement;
        SetDirection();
    }

    private void SetDirection()
    {
        Vector2 rightPos = transform.position;
        Vector2 leftPos = transform.position;
        rightPos.x += halfWidth;
        leftPos.x -= halfWidth;

        // Check for wall collisions and change direction
        if (CurrentDirection > 0) // Moving right
        {
            // Check for wall on the right
            if (Physics2D.Raycast(transform.position, Vector2.right, halfWidth + 0.1f, LayerMask.GetMask("Ground")))
            {
                CurrentDirection *= -1;
                spriteRenderer.flipX = true;
                return; // Exit early after changing direction
            }
            
            // Check for ledge on the right
            if (!Physics2D.Raycast(rightPos, Vector2.down, halfHeight + 0.1f, LayerMask.GetMask("Ground")))
            {
                CurrentDirection *= -1;
                spriteRenderer.flipX = true;
                return; // Exit early after changing direction
            }
        }
        else if (CurrentDirection < 0) // Moving left
        {
            // Check for wall on the left
            if (Physics2D.Raycast(transform.position, Vector2.left, halfWidth + 0.1f, LayerMask.GetMask("Ground")))
            {
                CurrentDirection *= -1;
                spriteRenderer.flipX = false;
                return; // Exit early after changing direction
            }
            
            // Check for ledge on the left
            if (!Physics2D.Raycast(leftPos, Vector2.down, halfHeight + 0.1f, LayerMask.GetMask("Ground")))
            {
                CurrentDirection *= -1;
                spriteRenderer.flipX = false;
                return; // Exit early after changing direction
            }
        }
    }
}