using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileFire : MonoBehaviour
{
    public enum Move
    {
        InMove,
        GoodMove,
        Shoot
    };
    
    
    public AudioClip blowOutFireAudio;
    public AudioClip throwFireAudio;
    public AudioClip crookedFireAudio;
    public AudioClip gongAudio;

    public AudioSource audio;
    public Move state;
    public Vector3 personPosition = new Vector3();

    public void play_blow_out()
    {
        audio.Stop();
        audio.clip = blowOutFireAudio;
        audio.Play();
    }
    public void playGong()
    {
        audio.Stop();
        audio.clip = gongAudio;
        audio.Play();
    }
    public void playCrooked()
    {
        audio.Stop();
        audio.clip = crookedFireAudio;
        audio.Play();
    }
    public void releasePress()
    {
        if (state ==  Move.Shoot)
        {
            Destroy(gameObject,3);
            
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        play_blow_out();

    }

    public void setPersonPosition(Vector3 position)
    {
        personPosition = position;
    }

    private void playThrow()
    {
        audio.Stop();
        audio.clip = throwFireAudio;
        audio.Play();
    }
    public void shoot()
    {
        state = Move.Shoot;
        playThrow();

        
        GetComponent<Rigidbody>().velocity = 80f*(transform.position-personPosition);
            
    }
    

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(30 * Time.deltaTime, 40 * Time.deltaTime,
            50 * Time.deltaTime);
    }


}
