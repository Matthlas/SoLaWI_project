using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.iOS;

public class DoSomethingController : MonoBehaviour
{

    [SerializeField] private ParticleSystem InteractionCue = null;
    
    public void Action()
    {
        Debug.Log("ACTION");

        InteractionCue.Play();
        // Harvest
        // Water...
    }
}
