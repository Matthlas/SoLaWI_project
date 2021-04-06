using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour {


    

    [SerializeField] public float maxSize = 5f;
    [SerializeField] public float BaseGrowthRate = 0.1f;
    
    
    private Vector3 _growingDirection = new Vector3(0,1,0);
    private float currentGrowthRate;
    private float growthTickRate = 2f;
    
    public bool growingCondition = true;
    public bool readyToHarvest = false;
    
    void Start()
    {
        currentGrowthRate = BaseGrowthRate;
        InvokeRepeating("Grow", growthTickRate, growthTickRate);
        // There is also "Cancel Invoke" might be helpful. But changing the growing condition is already enough I think
    }
    
    void Grow() {
        //Only grow unitl max size
        if (this.transform.localScale.y > maxSize)
        {
            growingCondition = false;
            readyToHarvest = true;
        }
        
        if (!growingCondition) return;
        
        this.transform.localScale += (currentGrowthRate * _growingDirection);
    }

    public void water()
    {
        currentGrowthRate *= 4;
        Invoke("Dry", 5f);
    }
    
    private void Dry()
    {
        currentGrowthRate = BaseGrowthRate;
    }

    
}

