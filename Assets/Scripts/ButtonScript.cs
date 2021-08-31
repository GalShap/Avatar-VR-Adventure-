using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{

    [SerializeField] private GameObject managerObj;
    private ButtonManager _manager;

    private bool moving = false;

    private Vector3 initalPos;
    // Start is called before the first frame update
    void Start()
    {
        initalPos = transform.position;
        _manager = managerObj.GetComponent<ButtonManager>();

    }

    // Update is called once per frame
    void Update()
    {
        if (moving && Vector3.Distance(initalPos, transform.position) < 0.25f)
        {

            var speed = 0.05f;
            var move = transform.forward;
            transform.Translate(move * (speed * Time.deltaTime));
            
        }
    }

    public void Move()
    {
        moving = true;
    }
    private void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.CompareTag("Rock"))


        {
            _manager.got_hit(gameObject.tag);
            //Destroy(other.gameObject);
        } 
     
    }
}
