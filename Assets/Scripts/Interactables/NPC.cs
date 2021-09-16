using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Cryptography;
using Cinemachine;
using UnityEngine;
using Random = UnityEngine.Random;

public class NPC : InteractableItemBaseClass {

    
    private Plant _myPlant;
    //public GameObject _myPlant;
    [SerializeField] public Plant[] plantprefabs = new Plant[3]; //different kinds of plants

    //weeding variables
    [SerializeField] private GameObject _weedPrefab;
    private float weedSpanRate = 25f;
    private GameObject newWeed;
    private List<GameObject> weedList = new List<GameObject>();

    private bool watered = false;
    private SeedListener.PlantSeeds kindOfPlant;

    private Color inactiveColor = new Color(0.443f, 0.612f, 0.195f, 0.5f);
    private Color activeColor = new Color(0.39f, 0.06f, 0f, 1f);
    
    //finite state machine for plant
    public enum BedFSM
    {
        inactive,
        plain,
        planted
    }
    // default state
    public BedFSM bedMode = BedFSM.plain;

    private void Start()
    {
        if (bedMode == BedFSM.inactive)
        {
            gameObject.GetComponent<Renderer>().material.color = inactiveColor;
        }
        StartCoroutine(SpawnWeeds());
    }
    
    
    public override void OnInteract()
    {
        PlayerControllerAdapted.Mode mode = 
            GameObject.Find("Player").GetComponent<PlayerControllerAdapted>().getMode();
        //perform an action depending on the mode
        if (bedMode != BedFSM.inactive)
        {

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
        }
        if (mode == PlayerControllerAdapted.Mode.Buddeln)
        {
            Dig();
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
                _myPlant.Water();
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
            Invoke("Dry", 30f);
            
        
            //if we have plant water plant
            if (_myPlant != null)
            {
                _myPlant.Water();
            }
            else
            {
                bedMode = BedFSM.plain;
            }
        }
    }

    private void Dry()
    {
        if (_myPlant != null)
        {
            _myPlant.Dry();
        }
        else
        {
            bedMode = BedFSM.plain;
        }
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
            /*//if first watered then weeded, check if now plant can grow
            if (watered && !_myPlant.growingCondition && (_myPlant.size < _myPlant.maxSize))
            {
                _myPlant.Water();
            }*/
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
        if (bedMode == BedFSM.inactive)
        {
            gameObject.GetComponent<Renderer>().material.color = activeColor;
            gameObject.transform.position += new Vector3(0f, 0.3f, 0f);

        }
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
            yield return new WaitForSeconds(Random.Range(weedSpanRate, weedSpanRate+15f));
            if (bedMode != BedFSM.inactive)
            {
                newWeed = Instantiate(_weedPrefab,
                    transform.position + new Vector3(Random.Range(-0.4f, 0.4f), 0.1f, Random.Range(-0.4f, 0.4f)),
                    Quaternion.identity, this.transform);
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

}

