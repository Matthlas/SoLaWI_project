using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuitarHandler : InteractableItemBaseClass
{
    public AudioClip audio1;

    public AudioClip audio2;

    private bool whichAudio = true;

    float originalY;

    public float floatStrength = 1; 

    void Start()
    {
        this.originalY = this.transform.position.y;
    }

    void Update()
    {
        // Guitar floats when its not selected to indicate that its interactable
        if (!selected)
        {
            transform.position = new Vector3(transform.position.x,
                originalY + ((float) Math.Sin(Time.time) * floatStrength),
                transform.position.z);
        }
        
    }
    
    public override void OnInteract()
    {
        if (!gameObject.GetComponent<AudioSource>().isPlaying)
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
            gameObject.transform.parent.GetComponent<AudioSource>().time = 420f;
            gameObject.transform.parent.GetComponent<AudioSource>().Play();
            
            //start audio
            gameObject.GetComponent<AudioSource>().Play();
            whichAudio = !whichAudio;
        }
        else
        {
            gameObject.GetComponent<AudioSource>().Stop();

        }
    }
}
