using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    // created an empty object in which added the main camera as the child object
  // now the empty object acts as the focal point for the main camera  
    public float rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // up/down = +y/-y axis
        // forward/back = +z/-z axis
        // right/left = +x/-x axis
        float horizontalInput = Input.GetAxis("Horizontal2");
  // this methods updates the camera angle on y axis by 1 degree on every update call
  // or on every frame call      
  //##      transform.Rotate(Vector3.up,1);// transform.Rotate(Vector3,float angle)
 // this method updates the camera angle when we press A or D or <- or -> every time
 // we press the key  
        transform.Rotate(Vector3.down,rotationSpeed * Time.deltaTime * horizontalInput);
    }
}
