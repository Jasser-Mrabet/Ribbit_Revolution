using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{


        [SerializeField] private float speed = 5f;
        [SerializeField] private SpriteRenderer spriteRenderer;

        [SerializeField] private Animator  animator;


        public float wallJumpCooldown  {get; set;}

        private Vector2 movement;

        private Vector2 screenBounds;

        private float playerHalfWidth;

        private float xPosLastFrame;

        private void Start()
        {

                screenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
                playerHalfWidth = spriteRenderer.bounds.extents.x;

        }


        // Update is called once per frame
        void Update()
        {

                HandelMovement();
                //ClapMovement();
                FlipCharacterX();


                if (wallJumpCooldown > 0f ) {


                 wallJumpCooldown -= Time.deltaTime;        

                }
        }

        private void FlipCharacterX()
        {
                float input = Input.GetAxis("Horizontal");
                if (input > 0 && (transform.position.x > xPosLastFrame))
                {
                        // we are moving or facing right
                        spriteRenderer.flipX = false;
                }
                else if (input < 0 &&  (transform.position.x < xPosLastFrame))
                {
                        // we are moving or facing left
                        spriteRenderer.flipX = true;
                }
                xPosLastFrame = transform.position.x;
        }



        private void HandelMovement()
        {
                
                if (wallJumpCooldown > 0f) return ;
                float input = Input.GetAxis("Horizontal");
                movement.x = input * speed * Time.deltaTime;
                transform.Translate(movement);
           
        }


        private void ClapMovement()
        {
                float clampedX = Mathf.Clamp(transform.position.x, -screenBounds.x, screenBounds.x);
                Vector2 pos = transform.position;
                pos.x = clampedX;
                transform.position = pos;

        }

}

