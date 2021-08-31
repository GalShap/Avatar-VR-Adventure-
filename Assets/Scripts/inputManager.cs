using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inputManager : MonoBehaviour
{
    private static InputMaster myControls;
    
    // Start is called before the first frame update
    
    void Awake()
    {
        myControls = new InputMaster();
    }

    public InputMaster getControls()
    {
        return myControls;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
