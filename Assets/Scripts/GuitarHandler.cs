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


        }
        else
        {
            gameObject.GetComponent<AudioSource>().Stop();
            isPlaying = false;

        }
        
        
    }
}
