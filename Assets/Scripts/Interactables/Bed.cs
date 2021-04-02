using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : InteractableItemBaseClass {

    
    [SerializeField]
    private GameObject _plantPrefab;
    
    
    [SerializeField] public float interactionDelay = 20;
    private bool isPlanted = false;
    private float lastInteractionTime = 0f;

    public override void OnInteract()
    {

            
            if (!isPlanted) Plant();
            
            _interactionCue.Play();

            // GetComponent<Animator>().SetBool("open", mIsOpen);
        
    }
    
    private void Plant()
    {
        //bed spawns a new plant on top
        Instantiate(_plantPrefab, transform.position + new Vector3(0,0.1f,0),  Quaternion.identity, this.transform);
        Debug.Log("Planted");

        isPlanted = !isPlanted;
        InteractText += isPlanted ? "to plant" : "to harvest";
    }
}