using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnmanager : MonoBehaviour
{
    public GameObject [] obstaclePrefabs;

    public float startDelay = 2;
    public float repeatRate = 2;
    private Vector3 spawnPos;
    private PlayerBehaviour _playerBehaviour;
    // Start is called before the first frame update
    void Start()
    {
        _playerBehaviour = GameObject.Find("Player").GetComponent<PlayerBehaviour>();
        spawnPos = transform.position;
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
    }

    //Update is called once per frame
    void SpawnObstacle()
    {
        if(!_playerBehaviour._gameOver)
        {
        GameObject obstaclePrefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];
        Instantiate(obstaclePrefab, spawnPos, obstaclePrefab.transform.rotation);
        }
    }
}
