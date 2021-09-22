using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Basic Interface that each state must implement
public interface INPCState_Citizen1
{
    INPCState_Citizen1 DoState(NPC_Citizen1 npc);
}

