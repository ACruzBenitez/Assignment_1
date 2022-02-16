using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    // Start is called before the first frame update
    void OnTriggerStay2D(Collider2D other)
    {
    if (other.gameObject.CompareTag("Obstacles")){  
        gameObject.SetActive(false);  
    }
    }

}
