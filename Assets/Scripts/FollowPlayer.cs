using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
     public GameObject playerBody;
    [SerializeField] float mouse;
    [SerializeField] float turnSpeed = 10.0f;
    // PlayerController player;
    [SerializeField] Vector3 offset;
    Quaternion offsetY,offsetX;
    // Start is called before the first frame update
    void Start()
    {
        offset = new Vector3(0, 1, -5);
        //player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        offsetX = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * turnSpeed, Vector3.up);
       // offsetY = Quaternion.AngleAxis(Input.GetAxis("Mouse Y") * -turnSpeed, Vector3.right); 
        offset = offsetX * offset;// * offsetY;
        
        transform.position = playerBody.transform.position + offset; 
        mouse = Input.GetAxis("Mouse Y");
        transform.LookAt(playerBody.transform.position);
    }

    // Update is called once per frame
    void Update ()
    {
        transform.position = playerBody.transform.position + new Vector3(0, 1, -5);

    }
}
