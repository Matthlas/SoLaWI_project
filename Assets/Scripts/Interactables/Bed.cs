using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : InteractableItemBaseClass {

    
    [SerializeField]
    private Plant _plantPrefab;
    private Plant _myPlant;

    private Vector3 growthRate;
    private bool watered = false;
    
    
    //finite state machine for plant
    private enum BedFSM
    {
        plain,
        planted

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
        else if (bedMode == BedFSM.planted && !_myPlant.readyToHarvest)
        {
            Debug.Log("Watering");
            Watering();
        }
        else if (bedMode == BedFSM.planted && watered)
        {
            Debug.Log("Bed is chillin");
        }

        else if (bedMode == BedFSM.planted && _myPlant.readyToHarvest)
        {
            Debug.Log("Harvesting");
            Harvesting();
            bedMode = BedFSM.plain;
        }



        // GetComponent<Animator>().SetBool("open", mIsOpen);
        
    }
    
    public void Planting()
    {
        _myPlant = Instantiate(_plantPrefab, transform.position + new Vector3(0, 0.1f, 0), Quaternion.identity, this.transform);
        _interactionCue.Play();
            
    }
        
    public void Watering() 
    {
        _myPlant.gameObject.transform.localScale += (80*growthRate);
        _interactionCue.Play();
        watered = true;
        Invoke("Dry", 5f);
    }
        
    public void Harvesting()
    {
        Destroy(_myPlant.gameObject);
        _interactionCue.Play();
    }

    private void Dry()
    {
        watered = false;
    }

}

