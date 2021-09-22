using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Basic Interface that each state must implement
public interface INPCState
{
    INPCState DoState(NPC_Sheep npc);
}

