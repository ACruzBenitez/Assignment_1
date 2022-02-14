using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBehaviour : MonoBehaviour
{
    [Header("Player Movement")]
    public float verticalForce;

    [Header("Ground Detection")]
    public Transform groundCheck;
    public float groundRadius;
    public LayerMask groundLayerMask;
    public bool isGrounded;

    [Header("Animation Properties")]
    public Animator animator;
    private Rigidbody2D rigidbody2D;

    [Header("Canvas")]

    public GameObject Canvas;
    private bool canvasActive;

    [Header("GameOver")]
    private bool _gameOver = false;

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
        DisableCanvas();
        animator.SetInteger("AnimationState", 1);

    }

    void DisableCanvas() {
        if(canvasActive && Input.GetButtonDown("Jump"))
      Canvas.SetActive(false);
    }
    IEnumerator DelayRestart() {
    yield return new WaitForSeconds(2.0f);
    
    SceneManager.LoadScene("Level");
}
    private void Move()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayerMask);


        if (isGrounded)
        {
            float jump = Input.GetAxisRaw("Jump");

            //check if the player is moving
            if(jump == 0)
            {
                animator.SetInteger("AnimationState", 1);
            }
            if(jump > 0)
            {
             animator.SetInteger("AnimationState", 2);
            }
        
            Vector2 move = new Vector2( 0 , jump * verticalForce);
            rigidbody2D.AddForce(move);
        }
        if(_gameOver){
            animator.SetInteger("AnimationState", 3);
            Debug.Log("dead");
        }

    }
    

    //collisions
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Obstacles"){
        StartCoroutine(DelayRestart());
        _gameOver = true;
        }
        
        //if (col.gameObject.tag == "Item"){

		//	TakeDamage(-10);
        //}
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
    }
}
