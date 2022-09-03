using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoviment : MonoBehaviour
{

    private Rigidbody2D rb;

    private BoxCollider2D coll;
    private Animator anim;
    private SpriteRenderer sprite;
    private float dirX = 0f;
    [SerializeField] private float jumpForce;
    [SerializeField] private float movimentX;

    [SerializeField] private LayerMask jumpableGround;

    private enum MovimentState {idle, running, jumping, falling};

    [SerializeField] private AudioSource jumpSoundEfect;

    // Start is called before the first frame update
    private void Start()
    {
       jumpForce = 16f;
       movimentX = 7f;

       rb = GetComponent<Rigidbody2D>();
       anim = GetComponent<Animator>();
       sprite = GetComponent<SpriteRenderer>();
       coll = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    private void Update()
    { 
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * movimentX, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {

            rb.velocity = new Vector3(rb.velocity.x, jumpForce, 0);
            jumpSoundEfect.Play();


        }

        UpdatePlayerAnimation();
        
    }

    private void UpdatePlayerAnimation()
    {
        MovimentState state;

        if (dirX > 0f)
        {
            //right
            state = MovimentState.running;
            sprite.flipX =  false;

        }
        else if (dirX < 0f)
        {
            //left
            state = MovimentState.running;
            sprite.flipX =  true;
        }
        else
        {
             state = MovimentState.idle;
        }

        if (rb.velocity.y > .1f)
        {
            state  = MovimentState.jumping;

        }
        else if (rb.velocity.y < -.1f)
        {
            state  = MovimentState.falling;
        }

        anim.SetInteger("state", (int)state);
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}
