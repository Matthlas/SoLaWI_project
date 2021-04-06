using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : InteractableItemBaseClass {

    
    [SerializeField]
    private Plant _plantPrefab;
    private Plant _myPlant;



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
            Planting();
            bedMode = BedFSM.planted;
        }
        else if (bedMode == BedFSM.planted && !_myPlant.readyToHarvest)
        {
            Watering();
        }
        else if (bedMode == BedFSM.planted && _myPlant.readyToHarvest)
        {
            Harvesting();
            bedMode = BedFSM.plain;
        }
        
    }
    
    public void Planting()
    {
        _myPlant = Instantiate(_plantPrefab, transform.position + new Vector3(0, 0.1f, 0), Quaternion.identity, this.transform);
        _interactionCue.Play();

    }
        
    public void Watering() 
    {
        _myPlant.water();
        _interactionCue.Play();
        
    }
        
    public void Harvesting()
    {
        Destroy(_myPlant.gameObject);
        _interactionCue.Play();
    }

}

