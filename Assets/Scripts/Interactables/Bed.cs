using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : InteractableItemBaseClass {

    
    [SerializeField]
    private GameObject _plantPrefab;
    
    
    
    [SerializeField] 
    public float interactionDelay = 20;
    private bool isPlanted = false;
    private float lastInteractionTime = 0f;
    
    

    public override void OnInteract()
    {
        if (!isPlanted)
        {
            Instantiate(_plantPrefab, transform.position + new Vector3(0, 0.1f, 0), Quaternion.identity, this.transform);
            _interactionCue.Play();
            isPlanted = true;
        }
        else
        {
            Debug.Log("Bed is chillin");
        }



        // GetComponent<Animator>().SetBool("open", mIsOpen);
        
    }
    
    

}

