using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchScript : MonoBehaviour
{
    private bool is_hit = false;

    
    public AudioSource fire_audio;
    [SerializeField] private GameObject fire;
    [SerializeField] private GameObject managerObj;
    

    private TorchManager _manager;
    // Start is called before the first frame update
    void Start()
    {
        _manager = managerObj.GetComponent<TorchManager>();
     

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
       

        if (is_hit == false && other.gameObject.CompareTag("Fire"))

        {   is_hit = true;
            Vector3 fire_pos = transform.position;
            
            fire_pos.y += 1;
            fire_audio.Play();
            Instantiate(fire, fire_pos, transform.rotation);
            
            _manager.got_hit();
            //destory fire ball when hit
            Destroy(other.gameObject);
        } 
     
    }
}