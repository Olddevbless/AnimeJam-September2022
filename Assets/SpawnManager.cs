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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SpawnEnemies()
    {
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
    void SpawnBoss()
    {
        Instantiate(bossPrefab, spawnPosition, Quaternion.identity);
    }
}
