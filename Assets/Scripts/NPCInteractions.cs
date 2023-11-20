using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteractions : MonoBehaviour
{

    public void TriggerNPCInteraction(int npcID, int npcState)
	{
		switch (npcID)
		{
			case 0:
			case 1:
				MissionExample mission = GetComponent<MissionExample>();
				mission.MissionExampleSwitch(npcID, npcState);
				break;
		}
	}
}
