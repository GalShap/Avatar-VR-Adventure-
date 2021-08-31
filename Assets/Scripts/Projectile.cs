using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // 0 - init state, 1- waiting to be shot
    public enum Move
    {
        InMove,
        GoodMove,
        BadMove,
        Shoot
    };

    public AudioClip idleRockAudio;
    public AudioClip throwRockAudio;
    public AudioSource audio;
    public Move state;
    public Camera head;
    public Vector3 headPosInit;
    public InputMaster myControls;
    public Vector3 handPosition = Vector3.zero;



    // Start is called before the first frame update
    void Start()
    {
        head = GameObject.Find("Main Camera").GetComponent<Camera>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (state == Move.GoodMove)
        {
            transform.position = new Vector3(transform.position.x, headPosInit.y - 0.3f, transform.position.z);
            transform.Rotate(30 * Time.deltaTime, 50 * Time.deltaTime,
                60 * Time.deltaTime); //rotates 50 degrees per second around z axis
        }
    }

    public void play_idle()
    {
        audio.Stop();
        audio.clip = idleRockAudio;
        audio.Play();
    }

    public void SwitchMove(Move move)
    {
        state = move;
    }



IEnumerator hit_haptic()
{
    List<UnityEngine.XR.InputDevice> devices = new List<UnityEngine.XR.InputDevice>();
    UnityEngine.XR.InputDevices.GetDevicesWithRole(UnityEngine.XR.InputDeviceRole.RightHanded, devices);
    foreach (var device in devices)
    {
        UnityEngine.XR.HapticCapabilities capabilities;
        if (device.TryGetHapticCapabilities(out capabilities))
        {
            if (capabilities.supportsImpulse)
            {
                uint channel = 0;
                float amplitude = 1.0f;
                float duration = 0.1f;
                device.SendHapticImpulse(channel, amplitude, duration);
            }
        }
    }

    return null;
}
    
private void OnTriggerEnter(Collider other)
    {
        if (state == Move.GoodMove && other.gameObject.CompareTag("Hand"))
        {   
            
            state = Move.Shoot;
            other.GetComponent<HandScript>().standbyProjectile = false;
            transform.SetParent(null);
            Vector3 temp = new Vector3(transform.position.x, 0.5f * head.transform.position.y + 0.5f * transform.position.y, transform.position.z);
            if (handPosition == Vector3.zero)
            {
                Destroy(gameObject);
                return;
            }

            audio.Stop();
            audio.clip = throwRockAudio;
            audio.Play();
            
            GetComponent<Rigidbody>().velocity = 70f*(transform.position-handPosition);
            GetComponent<Rigidbody>().useGravity = true;
            Destroy(gameObject,3);

            StartCoroutine(hit_haptic());
            
        }
    }


}
