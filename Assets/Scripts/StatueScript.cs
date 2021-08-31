using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueScript : MonoBehaviour
{
    private bool rising = false;
    public AudioSource sounds;
    public AudioClip rising_audio;
    public AudioClip endGameSound;
    [SerializeField] public GameObject buttonManagerObj;
    [SerializeField] public GameObject mergeObj;
    [SerializeField] public GameObject gameCamera; 
    private ButtonManager _buttonManager;
    [SerializeField] private GameObject portalSparks;
    [SerializeField] private GameObject portal;
    private bool minPortal;
    private TextMesh text1;
    [SerializeField] private GameObject textObeject;

    // Start is called before the first frame update
    void Start()
    {
        text1 = textObeject.GetComponent<TextMesh>();

        _buttonManager = buttonManagerObj.GetComponent<ButtonManager>();
    }

    // Update is called once per frame

    void Update()
    {
        var speed = 0.6f;
        var move = new Vector3(0, 1, 0);
        if (rising && transform.position.y < 2.1f)
        {
            
            transform.Translate(move * (speed * Time.deltaTime));
        }
        else
        {
            if (rising)
            {
                sounds.Stop();
                
            }
        }

        if (minPortal)
        {
            portalSparks.transform.Translate(move * (4 * (speed * Time.deltaTime)));
            portal.transform.localScale *= 0.995f;
        }
    }

    
    
    public IEnumerator rise()
    {
        sounds.clip = rising_audio;
        sounds.Play();
        yield return new WaitForSeconds(1.5f);
        rising = true;
        _buttonManager.Activate();
        yield return new WaitForSeconds(7f);

        minPortal = true;
        
        yield return null;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Statue"))
        {
            

            StartCoroutine(mergeObj.GetComponent<MergeStatue>().Merge());
        }
    }
}
