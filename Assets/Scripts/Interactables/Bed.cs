using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Cinemachine;
using UnityEngine;

public class Bed : InteractableItemBaseClass {

    
    [SerializeField]
    private Plant _plantPrefab;
    private Plant _myPlant;

    [SerializeField] private GameObject _weedPrefab;
    private float weedSpanRate = 10f;
    private GameObject newWeed;
    private List<GameObject> weedList = new List<GameObject>();

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
        PlayerControllerAdapted.Mode mode = 
            GameObject.Find("Player").GetComponent<PlayerControllerAdapted>().getMode();
        //perform an action depending on the mode
        if (mode == PlayerControllerAdapted.Mode.Säen)
        {
            Planting();
            bedMode = BedFSM.planted;
        }
        else if (mode == PlayerControllerAdapted.Mode.Giessen)
        {
            Watering();
        }
        else if (mode == PlayerControllerAdapted.Mode.Jäten)
        {
            Weeding();
        }
        else if (mode == PlayerControllerAdapted.Mode.Ernten)
        {
            Harvesting();
            bedMode = BedFSM.plain;
        }
        else if (mode == PlayerControllerAdapted.Mode.Buddeln)
        {
            Dig();
            bedMode = BedFSM.plain;
        }
        
    }
    
    public void Planting()
    {
        if (bedMode == BedFSM.plain){   
            Weeding();
            _myPlant = Instantiate(_plantPrefab, transform.position + new Vector3(0, 0.1f, 0), Quaternion.identity, this.transform);
            if (watered)
            {
                _myPlant.Grow();
            }
            StartCoroutine(SpawnWeeds());

        }
    }
        
    public void Watering() 
    {
        if (watered == false)
        {
            watered = true;
            
            //Change color to darker (bed is wet)
            this.GetComponent<MeshRenderer>().material.SetFloat("_Metallic", 0.5f);
        
            //Dry after some time
            Invoke("Dry", 40f);
            
        
            //if we have plant water plant
            if (_myPlant != null)
            {
                _myPlant.Grow();
            
            }
            else
            {
                bedMode = BedFSM.plain;
            }
        }
    }

    private void Dry()
    {
        /*if (_myPlant != null)
        {
            _myPlant.Dry();
        }
        else
        {
            bedMode = BedFSM.plain;
        }*/
        //Change color to lighter
        this.GetComponent<MeshRenderer>().material.SetFloat("_Metallic", 0f);
        watered = false;
    }

    public void Weeding()
    {

        for (int i = 0; i < weedList.Count; i++)
        {
            Destroy(weedList[i].gameObject);
        }

        if (_myPlant != null)
        {
            _myPlant.obstructiveWeeds = 0;
            //if first watered then weeded, check if now plant can grow
            if (watered && !_myPlant.growingCondition)
            {
                _myPlant.Grow();
            }
        }
        else
        {
            bedMode = BedFSM.plain;
        }


    }
    
    public void Harvesting()
    {
        if (_myPlant != null)
        {
            Destroy(_myPlant.gameObject);
            if (_myPlant.readyToHarvest)
            {
                _interactionCue.Play();
                //Score erhöhen für die Art der Pflanze
            }
        }
        bedMode = BedFSM.plain;
        
    }

    public void Dig()
    {
        if (_myPlant != null)
        {
            Destroy(_myPlant.gameObject);
            
        }
        bedMode = BedFSM.plain;
        Weeding();
        
    }
    
    
    IEnumerator SpawnWeeds()
    {
        while (true)
        {
            yield return new WaitForSeconds(weedSpanRate); 
        newWeed = Instantiate(_weedPrefab, transform.position + new Vector3(Random.Range(-0.4f, 0.4f), 0.1f, Random.Range(-0.4f, 0.4f)), Quaternion.identity, this.transform); 
        weedList.Add(newWeed);
        if (_myPlant != null)
        {
            _myPlant.obstructiveWeeds += 1;
        }
        else
        {
            bedMode = BedFSM.plain;
        }
        
        }
    }

}

