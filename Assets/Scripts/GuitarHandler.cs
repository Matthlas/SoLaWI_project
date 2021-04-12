using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuitarHandler : InteractableItemBaseClass
{
    public AudioClip audio1;

    public AudioClip audio2;

    private bool whichAudio = true;

    private bool isPlaying = false;
    
    float originalY;

    public float floatStrength = 1; 

    void Start()
    {
        this.originalY = this.transform.position.y;
    }

    void Update()
    {
        if (!selected)
        {
            transform.position = new Vector3(transform.position.x,
                originalY + ((float) Math.Sin(Time.time) * floatStrength),
                transform.position.z);
        }
    }
    
    public override void OnInteract()
    {
        if (!isPlaying)
        {
            if (whichAudio)
            {
                gameObject.GetComponent<AudioSource>().clip = audio1;
            }
            else
            {
                gameObject.GetComponent<AudioSource>().clip = audio2;
            }
            //set Soundtrack on later Point so it doesn't disturb
            gameObject.transform.parent.GetComponent<AudioSource>().time = 360f;
            gameObject.transform.parent.GetComponent<AudioSource>().Play();
            
            //start audio
            gameObject.GetComponent<AudioSource>().Play();

            whichAudio = !whichAudio;
            isPlaying = true;
            StartCoroutine(waitTilEnd());
        }
        else
        {
            gameObject.GetComponent<AudioSource>().Stop();
            isPlaying = false;

        }
        
    }
    public IEnumerator waitTilEnd()
    {
        yield return new WaitForSeconds(35);
        isPlaying = false;

    }
}
