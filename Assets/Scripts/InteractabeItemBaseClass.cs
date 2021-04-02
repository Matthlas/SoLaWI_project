using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnumItemType
{
    Default,
    Bed,
    Plant,
    Guitar
}

public class InteractableItemBaseClass : MonoBehaviour
{
    public string Name;

    public Sprite Image;

    public string InteractText = "Press F to pickup the item";

    public EnumItemType ItemType;

    public virtual void OnInteractAnimation(Animator animator)
    {
        animator.SetTrigger("tr_pickup");
    }

    public virtual void OnInteract()
    {
    }

    public virtual bool CanInteract(Collider other)
    {
        return true;   
    }
}