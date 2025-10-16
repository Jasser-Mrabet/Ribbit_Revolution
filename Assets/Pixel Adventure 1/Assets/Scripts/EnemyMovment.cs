using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovment : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float speed = 3f;
    [SerializeField] private int startDirection = 1;

    private int CurrentDirection;
    private float halfWidth;
    private Vector2 movement;

    private void Start()
    {
        halfWidth = spriteRenderer.bounds.extents.x;
        CurrentDirection = startDirection;
        spriteRenderer.flipX = startDirection == 1 ? false : true;
    }

    private void FixedUpdate()
    {

        movement.x  = CurrentDirection * speed;
        movement.y = rigidBody.velocity.y;
        rigidBody.velocity =movement;
        SetDirection();
    }


    private void SetDirection()
    {
    if (Physics2D.Raycast(transform.position, Vector2.right, halfWidth + 0.1f, LayerMask.GetMask("Ground")) && rigidBody.velocity.x > 0)
       
{

CurrentDirection *= -1;
spriteRenderer.flipX = true;

}

else if (Physics2D.Raycast(transform.position, Vector2.left, halfWidth + 0.1f, LayerMask.GetMask("Ground")) && rigidBody.velocity.x < 0) {

CurrentDirection *= -1;
spriteRenderer.flipX = false;

}


    }
}
