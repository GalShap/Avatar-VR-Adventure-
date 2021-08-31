using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueHead : MonoBehaviour
{


    public bool rotate = true;
    private bool notDestroy = true;
    [SerializeField] private Vector3 platform_pos;
    // Update is called once per frame
    void Update()
    {
        if (rotate)
        {
            transform.Rotate(0, 40 * Time.deltaTime, 0);
        }

        if (transform.position.y < 2.3f)
        { 
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            transform.position = platform_pos;
            Debug.Log("change position!");
        }
        
    }

    public void Activate()
    {
        rotate = false;
        GetComponent<Rigidbody>().useGravity = true;
     
    }
}
