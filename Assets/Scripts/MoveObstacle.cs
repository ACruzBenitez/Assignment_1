using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObstacle : MonoBehaviour
{
    public float speed = 30; 
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left*speed*Time.deltaTime); 
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player" || col.gameObject.tag == "Offscreen"){
        Destroy(gameObject);
        }
        
    }
    void OnTriggerStay2D(Collider2D other)
    {
    if (other.gameObject.CompareTag("Shield")){  
        Destroy(gameObject);    
    }
    }
    //void OnTriggerStay2D(Collider2D other)
    //{
    //if (other.gameObject.CompareTag("Offscreen")){  
    //    Destroy(gameObject);    
    //}
    //}
}
