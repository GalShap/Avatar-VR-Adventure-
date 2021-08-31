using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class AbeAudioManager : MonoBehaviour
{
    public AudioClip abe_warning;

    public AudioSource abe_audio_src;

   
    private void OnCollisionEnter(Collision other)
    {
       
        abe_audio_src.Stop();
        abe_audio_src.clip = abe_warning;
        abe_audio_src.Play();

    }
}
