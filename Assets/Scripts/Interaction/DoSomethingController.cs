using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.iOS;

public class DoSomethingController : MonoBehaviour
{

    [SerializeField] 
    private ParticleSystem InteractionCue = null;
    
    //[SerializeField]
    //private GameObject _plantPrefab;
    
    //var instance : GameObject = Instantiate(Prefabs.Load("Plant"));
    
    //finite state machine for bed
    private enum BedFSM
    {
        plain,
        planted,
        growth
        
    }
    // default state
    BedFSM bedMode = BedFSM.plain;
    
    
    
    ////action for testing
    public void Action()
    {
        Debug.Log("ACTION");

        
        
        //perform an action depending on the mode
        if (bedMode == BedFSM.plain)
        {
            InteractionCue.Play();
            Planting();
            bedMode = BedFSM.planted;

        }
        
        else if (bedMode == BedFSM.planted)
        {
            InteractionCue.Play();
            Watering();
            bedMode = BedFSM.growth;

        }
        
        else if (bedMode == BedFSM.growth)
        {
            InteractionCue.Play();
            Harvesting();
            bedMode = BedFSM.plain;

        }
    }
    
 
    public void Planting()
    {
        Debug.Log("Planting");
        GameObject plant = (GameObject)Instantiate(Resources.Load("Plant")); 
        Instantiate(plant, transform.position + new Vector3(0, -0.3f, 0), Quaternion.identity, this.transform);


    }

    public void Watering()
    {
        Debug.Log("Watering");
    }
    
    public void Harvesting()
    {
        Debug.Log("Harvesting");
    }
 
}
