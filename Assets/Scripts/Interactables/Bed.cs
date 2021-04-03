using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : InteractableItemBaseClass {

    
    [SerializeField]
    private GameObject _plantPrefab;
    
    //_plantPrefab = (GameObject)Instantiate(Resources.Load("Plant"));
    
    
    [SerializeField] public float interactionDelay = 20;
    private bool isPlanted = false;
    private float lastInteractionTime = 0f;
    
    //finite state machine for bed
    private enum BedFSM
    {
        plain,
        planted,
        growth
            
    }
    // default state
    BedFSM bedMode = BedFSM.plain;

    public override void OnInteract()
    {
        
        //perform an action depending on the mode
        if (bedMode == BedFSM.plain)
        {
            Planting();
            bedMode = BedFSM.planted;
            Debug.Log("Planting");
    
        }
            
        else if (bedMode == BedFSM.planted)
        {
            Watering();
            bedMode = BedFSM.growth;
            Debug.Log("Watering");
    
        }
            
        else if (bedMode == BedFSM.growth)
        {
            Harvesting();
            bedMode = BedFSM.plain;
            Debug.Log("Harvesting");
        }

        

            // GetComponent<Animator>().SetBool("open", mIsOpen);
        
    }
    
    
        public void Planting()
        {
            Debug.Log("Planting");
            GameObject plant = (GameObject)Instantiate(Resources.Load("Plant")); 
            Instantiate(plant, transform.position + new Vector3(0, 0.1f, 0), Quaternion.identity, this.transform);
            //Instantiate(_plantPrefab, transform.position + new Vector3(0, 0.1f, 0), Quaternion.identity, this.transform);
            
            _interactionCue.Play();
            
        }
    
        public void Watering()
        {
            Debug.Log("Watering");
            
            _interactionCue.Play();
        }
        
        public void Harvesting()
        {
            Debug.Log("Harvesting");
            
            _interactionCue.Play();
        }
        

}


