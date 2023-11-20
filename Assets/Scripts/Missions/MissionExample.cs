using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class MissionExample : MonoBehaviour
{
	[SerializeField] InteractiveObject npc1;
	[SerializeField] InteractiveObject npc2;
	[SerializeField] DialogueManager dialogueManager;

	[SerializeField] Actor[] Actors;

	[Header("NPC 1 Dialogue")]
	[SerializeField] Message[] TalkTo1;

	[Header("NPC 1 Dialogue")]
	[SerializeField] Message[] TalkTo0;
	[SerializeField] Message[] UTalkedTo0;

    public void  MissionExampleSwitch(int npcID, int npcState)
	{
		switch (npcID)
		{
			case 0:
				FirstNPC(npcState);
				break;
			case 1:
				SecondNPC(npcState);
				break;
		}
	}

	void FirstNPC(int npcState)
	{
		switch (npcState)
		{
			case 0:
				dialogueManager.StartDialogue(TalkTo1, Actors);
				npc2.npcState = 1;
				break;
		}
	}

	void SecondNPC(int npcState)
	{
		switch(npcState)
		{
			case 0:
				dialogueManager.StartDialogue(TalkTo0, Actors);
				break;
			case 1:
				dialogueManager.StartDialogue(UTalkedTo0, Actors);
				break;
			default:
				break;
		}
	}

}
