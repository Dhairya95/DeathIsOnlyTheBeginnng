using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody enemyRb;
    private GameObject player;
    private float speed = 5.0f;
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
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
     //    Debug.Log("lookDirection  = "+(player.transform.position - transform.position));
     //   Debug.Log("lookDirection normalized = "+(player.transform.position - transform.position).normalized);
// we want enemy to move towards the player to we will have to add the force in the direction
// of the player. To get player's direction , we'll subtract player's position and the
// enemy's position  to get that vector        
        enemyRb.AddForce(lookDirection * speed);
   // "normalized" normalizes the vector since if the distance between the player and the 
 // enemy increases , the force applied increases as well, so we are just normalizing it
 // so that the force stays the same or constant      

    if(transform.position.y < -2)
    {
        Destroy(gameObject);
        GameManager.score += 5;
    }
       }
    }
}
