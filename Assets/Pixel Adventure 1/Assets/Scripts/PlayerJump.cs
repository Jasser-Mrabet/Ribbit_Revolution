using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{

[SerializeField] private Rigidbody2D rigidBody;

[SerializeField] private SpriteRenderer spriteRenderer;

[SerializeField] private float jumpForce = 6;

[SerializeField] private float doublejumpForce = 6;


    private float playerHalfHeight;

    private bool canDoubleJump;

    private void Start()

    {

        playerHalfHeight = spriteRenderer.bounds.extents.y;

    }




    // Update is called once per frame
    void Update()
    {


        if (Input.GetButtonDown("Jump") && !GetIsGrounded())
        {
            Jump(jumpForce);
        }

        else if (Input.GetButtonDown("Jump") && !GetIsGrounded() && canDoubleJump  )
        {
            rigidBody.velocity = Vector2.zero;
            rigidBody.angularVelocity = 0;
            Jump(doublejumpForce);
            canDoubleJump = false;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        GetIsGrounded();

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

private void Jump(float force)
{
    
   
        rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
  


}
}

