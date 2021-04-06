using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour {


    

    [SerializeField] public float maxSize = 6f;
    [SerializeField] public float BaseGrowthRate = 0.1f;
    
    
    private Vector3 _growingDirection = new Vector3(0,1,0);
    private float currentGrowthRate;
    private float growthTickRate = 2f;
    
    private bool watered = false;

    public int obstructiveWeeds = 0;
    
    public bool growingCondition = true; //Perhaps unnecessary
    public bool readyToHarvest = false;
    
    void Start()
    {
        currentGrowthRate = BaseGrowthRate;
        InvokeRepeating("Grow", growthTickRate, growthTickRate);
        // There is also "Cancel Invoke" might be helpful. But changing the growing condition is already enough I think. Could also be replaced by a coroutine
    }
    
    void Grow() {
        //Only grow until max size
        if (this.transform.localScale.y >= maxSize)
        {
            growingCondition = false;
            readyToHarvest = true;
        }

        if (obstructiveWeeds > 3) return;
        
        if (!growingCondition) return;
        
        this.transform.localScale += (currentGrowthRate * _growingDirection);
    }

    public void Water()
    {
        if (watered) return;

        watered = true;
        currentGrowthRate *= 4;
    }
    
    public void Dry()
    {
        watered = false;
        currentGrowthRate = BaseGrowthRate;
    }

    
}

