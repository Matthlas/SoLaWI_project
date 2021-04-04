using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : InteractableItemBaseClass {


    [SerializeField] 
    private Vector3 growthRate;
   
    
    [SerializeField] 
    public float interactionDelay = 20;
   
    private bool isPlanted = false;
    private float lastInteractionTime = 0f;
    
    //finite state machine for plant
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
            Debug.Log("Planting");
            Planting();
            bedMode = BedFSM.planted;
        }
        else if (bedMode == BedFSM.planted)
        {
            Debug.Log("Watering");
            Watering();
            bedMode = BedFSM.growth;
        }
            
        else if (bedMode == BedFSM.growth)
        {
            Debug.Log("Harvesting");
            Harvesting();
            bedMode = BedFSM.plain;
        }

        

            // GetComponent<Animator>().SetBool("open", mIsOpen);
        
    }
    
    
        public void Planting()
        {
            this.transform.localScale += growthRate;
            _interactionCue.Play();
            
        }
        
        public void Watering() 
        {
            this.transform.localScale += (80*growthRate);
            _interactionCue.Play();
        }
        
        public void Harvesting()
        {
            this.transform.localScale -= (81*growthRate);
            _interactionCue.Play();
        }
        

}

