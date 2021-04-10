using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Cinemachine;
using UnityEngine;
using Random = UnityEngine.Random;

public class Bed : InteractableItemBaseClass {

    
    [SerializeField]
    //public Plant _plantPrefab;
    private Plant _myPlant;
    //public GameObject _myPlant;
    [SerializeField] public Plant[] plantprefabs = new Plant[3]; //different kinds of plants

    //weeding variables
    [SerializeField] private GameObject _weedPrefab;
    private float weedSpanRate = 10f;
    private GameObject newWeed;
    private List<GameObject> weedList = new List<GameObject>();

    private bool watered = false;
    private SeedListener.PlantSeeds kindOfPlant;
    //finite state machine for plant
    private enum BedFSM
    {
        plain,
        planted
    }
    // default state
    BedFSM bedMode = BedFSM.plain;

    private void Start()
    {
        StartCoroutine(SpawnWeeds());
    }


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
            
            kindOfPlant = SeedListener.getCurrentPlant();

            switch (kindOfPlant)
            {
                case SeedListener.PlantSeeds.Beet:
                    _myPlant = Instantiate(plantprefabs[0], transform.position + new Vector3(0, 0.1f, 0), Quaternion.identity,
                        this.transform);
                    _myPlant.transform.localScale = new Vector3(1f, 1f, 1f);
                    break;
            
                case SeedListener.PlantSeeds.Tomato:
                    _myPlant = Instantiate(plantprefabs[1], transform.position + new Vector3(0, 0.1f, 0), Quaternion.identity,
                        this.transform);
                    _myPlant.transform.localScale = new Vector3(1f, 1f, 1f);
                    break;
                
                case SeedListener.PlantSeeds.Cabbage:
                    _myPlant = Instantiate(plantprefabs[2], transform.position + new Vector3(0, 0.1f, 0), Quaternion.identity,
                        this.transform);
                    _myPlant.transform.localScale = new Vector3(1f, 1f, 1f);
                    break;
                
            }
            _myPlant.begin(kindOfPlant);
    

            // if bed is wet and we plant something plant instantly grows
            if (watered)
            {
                _myPlant.Grow();
            }

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
                
                if (_myPlant.readyToHarvest)
                    {
                        _interactionCue.Play();
                    }
                
                
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
        weedList.Clear();

        if (_myPlant != null)
        {
            _myPlant.obstructiveWeeds = 0;
            //if first watered then weeded, check if now plant can grow
            if (watered && !_myPlant.growingCondition && (_myPlant.size < _myPlant.maxSize))
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
            
            if (_myPlant.readyToHarvest)
            {
                ScoreboardHandler.newScore(_myPlant.getKind(), 1);
            }
            Destroy(_myPlant.gameObject);
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

