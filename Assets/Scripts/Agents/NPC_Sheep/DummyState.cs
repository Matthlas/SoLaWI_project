// using UnityEngine;
// using UnityEngine.AI;
//
// public class DummyState : INPCState
// {
//     public INPCState DoState(NPC_Sheep npc)
//     {
//         //Guarantee NavMeshAgent
//         if (npc.navAgent == null)
//             npc.navAgent = npc.GetComponent<NavMeshAgent>();
//         
//         // Excecute the state specific action
//         DoStateSpecificAction(npc);
//         
//         // Specify conditions for state transitions
//         if (SomeContidion(npc))
//             return npc.basicState;
//         else
//             return npc.otherState;
//     }
//
//     private void DoStateSpecificAction(NPC_Sheep npc)
//     {
//     }
//     
//     private bool SomeContidion(NPC_Sheep npc)
//     {
//         return true;
//     }
//     
// }