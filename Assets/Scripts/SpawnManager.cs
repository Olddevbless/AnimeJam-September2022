using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] float spawnDelay;
    [SerializeField] int enemiesToSpawn;
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject bossPrefab;
    Vector3 spawnPosition;
    void Start()
    {
        InvokeRepeating("SpawnEnemies", 2f, spawnDelay);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SpawnEnemies()
    {
        Instantiate(enemyPrefab, this.transform.position, Quaternion.identity);
    }
    void SpawnBoss()
    {
        Instantiate(bossPrefab, transform.position, Quaternion.identity) ;
    }
}
