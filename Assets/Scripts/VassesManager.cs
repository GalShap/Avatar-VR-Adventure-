using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VassesManager : MonoBehaviour
{
    private int num_of_hits = 0;
    public AudioSource abeAudio;
    public AudioClip first_hit;
    public AudioClip second_hit;
    public AudioClip last_hit;
    public AudioClip broken_vase;

    public GameObject script_obj;
    private ScreenFader fader;

  
    public IEnumerator got_hit()
    {
        abeAudio.Stop();
        abeAudio.clip = broken_vase;
        abeAudio.Play();

        yield return new WaitForSeconds(0.5f);
        num_of_hits += 1;
        switch (num_of_hits)
        {
            case 1:
                abeAudio.Stop();
                abeAudio.clip = first_hit;
                abeAudio.Play();
                break;
            case 3:
                abeAudio.Stop();
                abeAudio.clip = second_hit;
                abeAudio.Play();
                break;
            case 4:
                abeAudio.Stop();
                abeAudio.clip = last_hit;
                abeAudio.Play();
                StartCoroutine(ChangeScene());
                break;
        }
    }

   void foo()
   {
       SceneManager.LoadScene("Scenes/Temple Scene",  LoadSceneMode.Single);
   }
   
   IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(5);
        fader = script_obj.GetComponent<ScreenFader>();
        fader.FadeOut();
        Invoke("foo",  2f);

    }
}
