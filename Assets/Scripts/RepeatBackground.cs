using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    private Vector3 startposition;
    private float repeatwidth;
    //private playerController _playerController;

    // Start is called before the first frame update
    void Start()
    {
       // _playerController = GameObject.Find("player").GetComponent<playerController>();
        startposition = transform.position;
        repeatwidth = GetComponent<BoxCollider>().size.x*2;
    }

    // Update is called once per frame
    void Update()
    {
        if(startposition.x - transform.position.x > repeatwidth )
        {
            transform.position = startposition;
        }
    }
}
