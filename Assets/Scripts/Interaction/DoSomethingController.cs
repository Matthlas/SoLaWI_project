using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.iOS;

public class DoSomethingController : MonoBehaviour
{

    [SerializeField] 
    private ParticleSystem InteractionCue = null;
    
    [SerializeField]
    private GameObject _plantPrefab;
    
    //action for testing
    public void Action()
    {
        Debug.Log("ACTION");

        InteractionCue.Play();
        // Harvest
        // Water...
    }
    
    //use "p" for planting
    public void Plant()
    {
        Debug.Log("Planting");
        //bed spawns a new plant on top
        Instantiate(_plantPrefab, transform.position + new Vector3(0,-0.3f,0),  Quaternion.identity, this.transform);

        InteractionCue.Play();
        
    }
    
    //watering
    
    //planting
}
