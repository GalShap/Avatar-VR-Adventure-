using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbeCloudMovement : MonoBehaviour
{
    // Start is called before the first frame update
    private float up = 6.2f;
    private float down = 5.8f;
    private bool rising = true;
    private float speed = 0.2f;
    
    // Update is called once per frame
    void Update()
    {
        if (rising && transform.position.y < up)
        {
           
            transform.Translate(Vector3.up * (speed * Time.deltaTime));
        }

        else if (transform.position.y >= up)
        {
            rising = false;
        }
        
        if (!rising && transform.position.y > down)
        {
            
            transform.Translate(Vector3.down * (speed * Time.deltaTime));
        }
        
        else if (transform.position.y <= down)
        {
            rising = true;
        }
    }
}
