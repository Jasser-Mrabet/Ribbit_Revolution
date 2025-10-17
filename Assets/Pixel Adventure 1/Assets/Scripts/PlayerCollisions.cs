using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float bounce = 6f;
    [SerializeField] private Rigidbody2D rigidBody;

    private float halfHeight;

    void Start()
    {
        halfHeight = spriteRenderer.bounds.extents.y;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            CollideWithEnemy(collision);
        }
    }

    private void CollideWithEnemy(Collision2D collision)
    {
        // Better method: Check contact points to see if player is landing on top of enemy
        foreach (ContactPoint2D contact in collision.contacts)
        {
            // If the contact point is at the top of the enemy (relative to player position)
            if (contact.normal.y > 0.7f) // Contact normal pointing up means player hit enemy from above
            {
                Vector2 velocity = rigidBody.velocity;
                velocity.y = 0f;
                rigidBody.velocity = velocity;
                rigidBody.AddForce(Vector2.up * bounce, ForceMode2D.Impulse);
                Destroy(collision.gameObject);
                return; // Exit after handling the stomp
            }
        }

        // If we get here, player didn't stomp from above
        Debug.Log("Player hit by enemy!");
        // Handle player damage here
    }
}