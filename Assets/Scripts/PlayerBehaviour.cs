using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBehaviour : MonoBehaviour
{
    [Header("Player Movement")]
    public float horizontalForce;
    public float verticalForce;

    [Header("Ground Detection")]
    public Transform groundCheck;
    public float groundRadius;
    public LayerMask groundLayerMask;
    public bool isGrounded;

    [Header("Animation Properties")]
    public Animator animator;

    private Rigidbody2D rigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
     
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayerMask);


        if (isGrounded)
        {
            float jump = Input.GetAxisRaw("Jump");
            float run = Input.GetAxisRaw("Horizontal");

            //check if the player is moving
                if (run != 0)
            {
                run = Flip(run);

            }
            else if(run == 0 && jump == 0)
            {
                animator.SetInteger("AnimationState", 0);
            }
            if(jump > 0)
            {
             animator.SetInteger("AnimationState", 2);
            }
        
            Vector2 move = new Vector2(run * horizontalForce, jump * verticalForce);
            rigidbody2D.AddForce(move);

            
        }
    }

    private float Flip(float x)
    {
        x = (x > 0) ? 1 : -1;

        transform.localScale = new Vector3(x, 1.0f);
        return x;
    }
    //collisions
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy"){
        
		//	TakeDamage(5);
        }
        //if (col.gameObject.tag == "Item"){

		//	TakeDamage(-10);
        //}
        if (col.gameObject.tag == "Offscreen"){

			SceneManager.LoadScene("Level");
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
    }
}