using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
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
    public GameObject CanvasText;
    public bool canvasActive;
    public bool textActive;


    [Header("Cameras")]
    public CinemachineVirtualCamera player;
    public CinemachineVirtualCamera zoom;


    [Header("GameOver")]
    public bool _gameOver;

    [Header("Abilities")]
    public GameObject shield;
    public GameObject scoretext;
    public float score = 0;



    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        canvasActive = true;
        textActive = false;
        _gameOver = false;
     
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
        DisableCanvas();
        if(_gameOver){
            animator.SetInteger("AnimationState", 3);
        }

        scoretext.GetComponent<Text>().text = score.ToString("F0"); 
    }

    void DisableCanvas() {
        if(canvasActive && Input.GetButtonDown("Submit"))
      Canvas.SetActive(false);
      CanvasText.SetActive(true);

    }
    IEnumerator DelayRestart() {
    yield return new WaitForSeconds(2.0f);
    
    SceneManager.LoadScene("Level");
}

    private void Move()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayerMask);

        float jump = Input.GetAxisRaw("Jump");


        if (isGrounded)
        {

            //check if the player is moving
            if(jump == 0)
            {
                animator.SetInteger("AnimationState", 1);
            }
        
            Vector2 move = new Vector2( 0 , jump * verticalForce);
            rigidbody2D.AddForce(move);
        }
            if(jump > 0)
            {
             animator.SetInteger("AnimationState", 2);
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
    void OnTriggerStay2D(Collider2D other)
    {
       if (other.gameObject.CompareTag("Zoom")){  
        zoom.Priority = 1;
        player.Priority = 0;
        }
        else{
        zoom.Priority = 0;
        player.Priority = 1;
        } 
        if (other.gameObject.CompareTag("Crown")){  
        shield.SetActive(true);
        score += 5;
        }
    }

    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
    }
}
