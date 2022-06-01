using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerAttack : MonoBehaviour
{
   public GameManager gameManager;
    public GameObject enemyPrefab;
    private float spawnRange = 50.0f;
    private int startDelay = 2;
    private float spawnInterval = 3f;
   
    // Start is called before the first frame update
    void Start()
    {
     InvokeRepeating("SpawnAttacker",startDelay,spawnInterval);
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    void SpawnAttacker()
    {   
      if(GameManager.isGameActive && gameManager.topFloor)
      {
            Instantiate(enemyPrefab,GenerateRandomPosition(),enemyPrefab.transform.rotation);
      } 
    }
    private Vector3 GenerateRandomPosition()
    {
        float spawnRangeX = Random.Range(-spawnRange,spawnRange+1.0f);
        float spawnRangeZ = Random.Range(-spawnRange,spawnRange+1.0f);
        Vector3 spawnPos = new Vector3(spawnRangeX,110,spawnRangeZ);
        return spawnPos;
    }
}
