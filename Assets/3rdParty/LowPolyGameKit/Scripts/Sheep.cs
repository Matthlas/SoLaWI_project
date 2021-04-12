using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Sheep : MonoBehaviour
{

    private Animator mAnimator;
    
    // Use this for initialization
    void Start()
    {
        mAnimator = GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update()
    {

        mAnimator.SetBool("walk", false);

    }
}
