using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NPCTask : MonoBehaviour
{
    public Vector3 location;
    public float workingTime = 5f;
    public bool active = false;
    public bool done = false;

    public void OnEnable()
    {
        location = this.transform.position;
    }

    public void StartTask()
    {
        Invoke("TaskDone", workingTime);
        active = true;
    }

    public void TaskDone()
    {
        active = false;
        done = true;
    }

}
