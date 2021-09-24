// using System;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.PlayerLoop;
//
//
// public class MeetableNPC : MonoBehaviour
// {
//     public string Name;
//
//     internal bool selected = false;
//
//     public virtual void OnInteractAnimation(Animator animator)
//     {
//         
//         //animator.SetTrigger("tr_pickup");
//         
//     }
//
//     public virtual void OnInteract()
//     {
//     }
//
//     public virtual void Select()
//     {
//         //this.GetComponent<MeshRenderer>().material.SetFloat("_Metallic", 0.5f);
//         this.transform.position += new Vector3(0,0.1f,0);
//         selected = true;
//     }
//
//     public virtual void Unselect()
//     {
//         //this.GetComponent<MeshRenderer>().material.SetFloat("_Metallic", 0f);
//         this.transform.position -= new Vector3(0,0.1f,0);
//         selected = false;
//     }
//
//     public virtual bool CanInteract(Collider other)
//     {
//         return true;   
//     }
//     
// }