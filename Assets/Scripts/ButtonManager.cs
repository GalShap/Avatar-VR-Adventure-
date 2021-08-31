using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    // Start is called before the first frame update
    private int num_of_hits = 0;
    public AudioSource sounds;
    
    public AudioClip land_sound;
    public AudioClip wind_sound;
    public AudioClip water_sound;
    public AudioClip fire_sound;
    public AudioClip flags_story;

    [SerializeField] public GameObject button1;
    [SerializeField] public GameObject button2;
    [SerializeField] public GameObject button3;
    [SerializeField] public GameObject button4;
    
    public AudioClip bad_sound;
    [SerializeField] public GameObject head_statue;
    private int waitingToButtonNumber = 1;
    private bool isActive = false;

    void Start()
    {
        //Activate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Activate()
    {
        Debug.Log("inside activate");
        isActive = true;
        sounds.Stop();
        sounds.clip = flags_story;
        sounds.Play();
        button1.GetComponent<ButtonScript>().Move();
        button2.GetComponent<ButtonScript>().Move();
        button3.GetComponent<ButtonScript>().Move();
        button4.GetComponent<ButtonScript>().Move();

    }
    public void got_hit(string buttonTag)
    {
        Debug.Log("in got hit");

        if (isActive == false) return;

        switch (buttonTag)
        {
            case "Button 1":
                waitingToButtonNumber = 2;
                sounds.Stop();
                sounds.clip = land_sound;
                sounds.Play();
                Debug.Log("land ok");

                break;
            case "Button 2":
                if (waitingToButtonNumber == 2)
                {
                    waitingToButtonNumber++;
                    sounds.Stop();
                    sounds.clip = wind_sound;
                    sounds.Play();

                }
                
                
                else 
                {
                    sounds.Stop();
                    sounds.clip = bad_sound;
                    sounds.Play();
                    waitingToButtonNumber = 1;
                }
                break;
            case "Button 3":
                if (waitingToButtonNumber == 3)
                {
                    waitingToButtonNumber++;
                    sounds.Stop();
                    sounds.clip = water_sound;
                    sounds.Play();

                }
                else
                {
                    sounds.Stop();
                    sounds.clip = bad_sound;
                    sounds.Play();
                    waitingToButtonNumber = 1;


                }
                break;
            case "Button 4":
                if (waitingToButtonNumber == 4)
                {
                    waitingToButtonNumber++;
                    sounds.Stop();
                    sounds.clip = fire_sound;
                    sounds.Play();
                    head_statue.GetComponent<StatueHead>().Activate();
                    
                }
                else
                {
                    sounds.Stop();
                    sounds.clip = bad_sound;
                    sounds.Play();
                    waitingToButtonNumber = 1;


                }
                break;
        }

        
    }
}
