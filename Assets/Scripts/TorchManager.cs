using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class TorchManager : MonoBehaviour
{   
    
    private int num_of_hits = 0;
    public GameObject statue;
    private StatueScript statueScript;
    private void Start()
    {
        Debug.Log("start TorchManager");
        Debug.Log(gameObject);
        statueScript = statue.GetComponent<StatueScript>();

    }

    

    public void got_hit()
    {
        num_of_hits += 1;
        
        
        
        if (num_of_hits == 3)
        {
            StartCoroutine(statueScript.rise());
        }
    }
}
