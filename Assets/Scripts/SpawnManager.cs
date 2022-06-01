using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameManager gameManager;
    private int index;
    public int enemyCount; // counts the number of enemies
    public GameObject []enemyPrefab;
    public GameObject powerupPrefab;
    private float spawnRange = 9.0f;
    // Start is called before the first frame update
    void Start()
    {
       // SpawnEnemyWave(GameManager.waveNumber);
    }
    public void SpawnEnemyWave(int enemiesToSpawn)
    {
        if(enemiesToSpawn % 5 == 0 && enemiesToSpawn != 0)
        {
            enemiesToSpawn--;
            Instantiate(enemyPrefab[2],GenerateRandomPosition(),enemyPrefab[2].transform.rotation);
        }
        for(int i=0;i<enemiesToSpawn;i++)
        {
            index = Random.Range(0,enemyPrefab.Length-1);
            Debug.Log("index  = "+index);
            Instantiate(enemyPrefab[index],GenerateRandomPosition(),enemyPrefab[index].transform.rotation);
        }

    }
    // Update is called once per frame
    void Update()
    {
        
        if(GameManager.isGameActive && GameManager.waveNumber <= GameManager.maxWave && !gameManager.topFloor)
        {
            
  // FindObjectOfType finds all the objects present in the screen. We can specify what type 
  // of object we want. here we specify the class name . Ex, Enemy (can be considered as
  // the script name but it actually is a class name).Its return type is an Integer type 
  // of Array    
// another method is FindObjectOfType()  . Object without an 's'  
        enemyCount = FindObjectsOfType<EnemyBoss>().Length + FindObjectsOfType<EnemyStrong>().Length + FindObjectsOfType<Enemy>().Length;// returns length of the array
      Debug.Log("enemyCount "+enemyCount);
        if(enemyCount == 0)
        {
            GameManager.waveNumber++;
            if(GameManager.waveNumber == 6)return;
            SpawnEnemyWave(GameManager.waveNumber);
            Instantiate(powerupPrefab,GenerateRandomPosition(),powerupPrefab.transform.rotation);
        }
        }
    }
    private Vector3 GenerateRandomPosition()
    {
        float spawnRangeX = Random.Range(-spawnRange,spawnRange+1.0f);
        float spawnRangeZ = Random.Range(-spawnRange,spawnRange+1.0f);
        Vector3 spawnPos = new Vector3(spawnRangeX,0,spawnRangeZ);
        return spawnPos;
    }
}
