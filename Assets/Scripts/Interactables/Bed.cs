using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : InteractableItemBaseClass {

    
    [SerializeField]
    private GameObject _plantPrefab;

    [SerializeField] 
    private Vector3 growthRate;
    //_plantPrefab = (GameObject)Instantiate(Resources.Load("Plant"));
    
    
    [SerializeField] 
    public float interactionDelay = 20;
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
            //GameObject plant = Instantiate(plant, transform.position + new Vector3(0, 0.1f, 0), Quaternion.identity, this.transform);
            
            Instantiate(_plantPrefab, transform.position + new Vector3(0, 0.1f, 0), Quaternion.identity, this.transform);
            //GameObject.Find("Seed").GetComponent<Plant>().Planting();
            _interactionCue.Play();
            
        }
        

        public void Watering()
                {
                    _plantPrefab.transform.localScale += growthRate;
                    _interactionCue.Play();
                }
        
        public void Harvesting()
        {
            Debug.Log("Harvesting");
            //Destroy(current_plant);
            _interactionCue.Play();
        }
        

}

