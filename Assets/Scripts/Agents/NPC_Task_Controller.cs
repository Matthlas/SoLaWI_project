using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class NPC_Task_Controller : MonoBehaviour
{
    public List<NPCTask> tasks = new List<NPCTask>();
    public NPCTask activeTask;

    private int TaskCounter = 0;

    private bool restingAfterDone = false;
    public float restingTimeBetweenTasks = 5f;
    // Start is called before the first frame update
    void OnEnable()
    {
        activeTask = tasks[TaskCounter];
    }

    // Update is called once per frame
    void Update()
    {
        if (activeTask.done && !restingAfterDone)
        {
            restingAfterDone = true;
            Invoke("SetNextTask", restingTimeBetweenTasks);
        }

    }

    private void SetNextTask()
    {
        TaskCounter += 1;
        activeTask = tasks[TaskCounter % tasks.Count];
        Debug.Log(activeTask);
        if (TaskCounter % tasks.Count == 0)
            RestTaskList();
        CancelInvoke("SetNextTask");
        restingAfterDone = false;
    }
    
    public NPCTask currentTask()
    {
        return activeTask;
    }

    private void RestTaskList()
    {
        foreach (var task in tasks)
        {
            task.done = false;
        }
    }
}
