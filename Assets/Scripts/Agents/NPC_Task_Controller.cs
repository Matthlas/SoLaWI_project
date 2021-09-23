using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class NPC_Task_Controller : MonoBehaviour
{
    public List<NPCTask> tasks = new List<NPCTask>();
    public NPCTask activeTask;

    private int TaskCounter = 0;
    // Start is called before the first frame update
    void OnEnable()
    {
        activeTask = tasks[TaskCounter];
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("TaskCounter ");
        Debug.Log(TaskCounter);
        Debug.Log("tasks.count ");
        Debug.Log(tasks.Count);
         
        if (activeTask.done)
        {
            TaskCounter += 1;
            
        }

        if (TaskCounter == tasks.Count)
        {
            TaskCounter = 0;
            foreach (var task in tasks)
            {
                task.done = false;
            }
        }
        activeTask = tasks[TaskCounter];
    }

    public NPCTask currentTask()
    {
        return activeTask;
    }
}
