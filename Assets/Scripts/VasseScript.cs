using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VasseScript : MonoBehaviour
{
    private bool is_hit = false;

    [SerializeField] private GameObject managerObj;

    private VassesManager _manager;
    // Start is called before the first frame update
    void Start()
    {
        _manager = managerObj.GetComponent<VassesManager>();
     

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
       

        if (is_hit == false && other.gameObject.CompareTag("Rock"))
        
        {
            StartCoroutine(_manager.got_hit());
            is_hit = true;
        } 
     
    }
}
