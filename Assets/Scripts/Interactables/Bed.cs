using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : InteractableItemBaseClass {

    
    [SerializeField]
    private Plant _plantPrefab;
    private Plant _myPlant;

    [SerializeField] private GameObject _weedPrefab;
    private bool _spawningWeeds = false;
    private float weedSpanRate = 2f;
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
        //perform an action depending on the mode
        if (bedMode == BedFSM.plain)
        {
            Planting();
            bedMode = BedFSM.planted;
        }
        // plant is still growing and unwatered, then we water
        else if (bedMode == BedFSM.planted && !_myPlant.readyToHarvest && !watered)
        {
            Watering();
        }
        //if its watered we remove the weeds
        else if (bedMode == BedFSM.planted && !_myPlant.readyToHarvest && watered)
        {
            Weeding();
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
        _spawningWeeds = true;
        StartCoroutine(SpawnWeeds());
        
        _interactionCue.Play();
    }
        
    public void Watering() 
    {
        _myPlant.Water();
        //Change color to darker (bed is wet)
        this.GetComponent<MeshRenderer>().material.SetFloat("_Metallic", 0.5f);
        watered = true;
        //Dry after some time
        Invoke("Dry", 5f);
        
        _interactionCue.Play();
    }

    private void Dry()
    {
        _myPlant.Dry();
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

        _myPlant.obstructiveWeeds = 0;
    }
    
    public void Harvesting()
    {
        Destroy(_myPlant.gameObject);
        _spawningWeeds = false;
        Weeding();
        
        _interactionCue.Play();
    }
    
    
    IEnumerator SpawnWeeds()
    {
        while (_spawningWeeds)
        {
            yield return new WaitForSeconds(weedSpanRate);
            newWeed = Instantiate(_weedPrefab, transform.position + new Vector3(Random.Range(-0.4f, 0.4f), 0.1f, Random.Range(-0.4f, 0.4f)), Quaternion.identity, this.transform);
            weedList.Add(newWeed);
            _myPlant.obstructiveWeeds += 1;
        }
        Weeding();
    }

}

