using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : MonoBehaviour
{ 
    private Rigidbody enemyRb;
    private GameObject player;
    private float speed = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
       if(GameManager.isGameActive)
       {
        Vector3 lookDirection = (player.transform.position - transform.position);    
        enemyRb.AddForce(lookDirection * speed);
        if(transform.position.y < -2)
        {
            Destroy(gameObject);
            GameManager.score += 50;
        }
       }
    }
}
