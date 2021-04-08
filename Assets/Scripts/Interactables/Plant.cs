using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{

    //basic parameters
    //private float humidity = 0f;
    //private bool watered = false;
    public int obstructiveWeeds = 0;
    public bool growingCondition = true;
    public bool readyToHarvest = false;
    //public float care = 0f;


    //plant specific parameters
    [SerializeField] public float maxSize = 6f;
    [SerializeField] public float GrowthRate = 2f;

    private Vector3 _growingDirection = new Vector3(0, 1, 0);
    //private float currentGrowthRate;
    //private float growthTickRate = 2f;
    //private float needed_care = 3f; //e.g how often we need to water, to harvest
    //private float health = 3f; //if health == 0 plant dies


    void Start()
    {
        StartCoroutine(checkIfDead());
        //InvokeRepeating("Grow", growthTickRate, growthTickRate);
        // There is also "Cancel Invoke" might be helpful. But changing the growing condition is already enough I think. Could also be replaced by a coroutine
    }
    

    public IEnumerator checkIfDead()
    {
        while (true)
        {
            yield return new WaitForSeconds(3);
        if (obstructiveWeeds == 7)
        {
            readyToHarvest = false;
            Destroy(this.gameObject);
        }
        }
    }
    

    public void Grow()
    {

        //Only grow until max size
        if (this.transform.localScale.y >= maxSize)
        {
            readyToHarvest = true;
        }

        //Weeds influence plant growth
        if (obstructiveWeeds >= 4)
        {
            growingCondition = false;
            this.GetComponent<MeshRenderer>().material.SetFloat("_Metallic", 0.5f);
        }
        else
        {
            growingCondition = true;
            this.GetComponent<MeshRenderer>().material.SetFloat("_Metallic", 0f);
        }
        

        if (!growingCondition) return;
        //if (this.transform.localScale.y <= ((maxSize / needed_care) * care)) // n deckel aufs wachsen draufpacken, wenn nicht genug care raufpacken

        this.transform.localScale += (GrowthRate * _growingDirection);
    }





    
    
   /* public void Dry()
    {
        watered = false;
    }*/


    
}

