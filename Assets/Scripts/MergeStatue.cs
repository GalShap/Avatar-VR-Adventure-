using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeStatue : MonoBehaviour
{
    [SerializeField] public GameObject gameCamera; 

    [SerializeField] public GameObject head;
    [SerializeField] public GameObject body;
    [SerializeField] public GameObject fullStatue;
    [SerializeField] public GameObject torando_prefab;
    [SerializeField] public GameObject sparksPrefab1;
    [SerializeField] public GameObject sparksPrefab2;
    public AudioSource sounds;
    public AudioClip endGameSound;
    [SerializeField] private GameObject textObeject;
    [SerializeField] private GameObject textObeject2;
    [SerializeField] private GameObject textObeject3;
    [SerializeField] private GameObject textObeject4;
    private TextMesh text1;
    private TextMesh text2;
    private TextMesh text3;
    private TextMesh text4;

    private GameObject tornado;
    private bool rising = false;

    private void Start()
    {
        text1 = textObeject.GetComponent<TextMesh>();
        text2 = textObeject2.GetComponent<TextMesh>();
        text3 = textObeject3.GetComponent<TextMesh>();
        text4 = textObeject4.GetComponent<TextMesh>();
        //StartCoroutine(Merge());
    }

    private void Update()
    {
        if (rising && tornado != null)
        {
            if (tornado.transform.position.y < 20f)
            {
                var speed = 1f;
                var move = new Vector3(0, 1, 0);
                tornado.transform.Translate(move * (speed * Time.deltaTime));
                 
            }
            else
            {
                rising = false;
                Destroy(tornado);
            }
            
        }
    }

    IEnumerator ActivateTornado(Vector3 pos, Quaternion rot )
    {

        tornado = Instantiate(torando_prefab,pos, rot);
        tornado.transform.localScale = new Vector3(0.5f,0.5f,0.5f);
        var temp = new WaitForSeconds(2f);
        
        rising = true;
        yield return null;

    }

    IEnumerator end_game_corrutine()
    {
        text1.text = "end game courotine";
        sounds.Stop();
        sounds.clip = endGameSound;
        sounds.Play();
        yield return new WaitForSeconds(15f);
        text1.text = "waitted 15";

        ScreenFader fader = gameCamera.GetComponent<ScreenFader>();
        fader.FadeOut();
       
    }
    public IEnumerator Merge()
    {
        text1.text = "inside merge";

        Vector3 pos = body.transform.position;
        Quaternion rot = body.transform.rotation;
        StartCoroutine(ActivateTornado(pos, rot));
        Destroy(head.gameObject);
        Destroy(body.gameObject);
        Instantiate(fullStatue, pos, rot);
        text1.text = "before wait 2 sec";
        //yield return new WaitForSeconds(1.5f);
        text1.text = "waited 2f seconds";
        Instantiate(sparksPrefab1, pos, Quaternion.identity);
        Instantiate(sparksPrefab2, pos, Quaternion.identity);
        text1.text = "calling end_game_corrutine";

        StartCoroutine(end_game_corrutine());
        yield return null;
    }
}
