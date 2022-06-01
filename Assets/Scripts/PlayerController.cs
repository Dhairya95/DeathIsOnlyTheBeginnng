using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject balanceCamera;
    public GameObject playerIndicator;
    private float powerupStrength = 50.0f;
    public bool hasPowerup;// by default it's false
    public GameObject focalPoint; // in order to get the focal point object from unity
    public float speed = 5.0f;
    private Rigidbody playerRb; // using physics for player movement
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();// getting the rigid body component
       // focalPoint = GameObject.Find("Focal Point");// getting the reference of Focal Point
    }
    IEnumerator PowerupCountdownRoutine(int time)
    {
        yield return new WaitForSeconds(time);// this thread waits for given seconds then moves to the next statement
        hasPowerup = false;
        playerIndicator.gameObject.SetActive(false);// setting the object active in Unity
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 offset = new Vector3(0,-0.5f,0);
        playerIndicator.transform.position = transform.position + offset; 
    }
    void FixedUpdate() // it's better to use FixedUpdate for smooth running of game   
    {
        if(GameManager.isGameActive)
        {

            if(gameManager.topFloor)
            {
            float forwardInput = Input.GetAxis("Vertical");
            //##   playerRb.AddForce(Vector3.forward * speed * forwardInput);// applying force to move forward
            // we want to point forward corresponding to the forward of the focal point of camera
            playerRb.AddForce(balanceCamera.transform.forward * speed * forwardInput);  
                float sidewardInput = Input.GetAxis("Horizontal");
                playerRb.AddForce(balanceCamera.transform.right * speed * sidewardInput);
            }
            else
            {
                    // when dealing with physics for movement
                // in unity -> edit -> project setting -> input manager -> axes -> vertical -> (name)
                float forwardInput = Input.GetAxis("Vertical");
            //##   playerRb.AddForce(Vector3.forward * speed * forwardInput);// applying force to move forward
            // we want to point forward corresponding to the forward of the focal point of camera
                playerRb.AddForce(focalPoint.transform.forward * speed * forwardInput);  
                float sidewardInput = Input.GetAxis("Horizontal");
                playerRb.AddForce(focalPoint.transform.right * speed * sidewardInput);

            }

        }
        else
        {
          //  transform.position = new Vector3(0,0.1f,0);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Powerup"))
        {
            hasPowerup = true;
            playerIndicator.gameObject.SetActive(true);// setting the object active in Unity
            StartCoroutine(PowerupCountdownRoutine(7));
            Destroy(other.gameObject);// destroying powerup whereas Destroy(gameObject)
            // destroys the player 
        }
    }
 // OnTriggerEnter - Is called whenever an object with a collider has passed through an
 // object with an "isTrigger" checked collider. OnCollisionEnter - Is called whenever
 // an object with a collider has collided with another object that contains a collider.
    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Enemy") && hasPowerup)
        {
     // getting Rigidbody component of enemy       
            Rigidbody enemyRb = other.gameObject.GetComponent<Rigidbody>();
// here we need to find direction from player to enemy so that we can shoot him back in 
// that direction only
           Vector3 enemyDirection = other.gameObject.transform.position - transform.position;
 // applying force to enemy in the opposite direction   
            enemyRb.AddForce(enemyDirection * powerupStrength,ForceMode.Impulse);
            Debug.Log("Collided with "+other.gameObject.name+" with powerup set to "+hasPowerup);
        }    

        if(other.gameObject.CompareTag("EndGame"))
        {
            GameManager.isGameActive=false;
            gameManager.gameOverNoPU.SetActive(true);
            Time.timeScale = 0;
        }
    }        
 }

