using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObject : MonoBehaviour
{
	public bool isNPC = true;

	[Header("NPC Settings")]
	public int npcID = 0;
	public int npcState = 0;

	[Header("Object Settings")]
	[SerializeField] int objectID = 0;
	[SerializeField] int objectState = 0;

	NPCInteractions npcInteractionsManager;

	private void Start()
	{
		npcInteractionsManager = FindObjectOfType<NPCInteractions>();
	}
	public void TriggerInteraction()
	{
		if (isNPC)
		{
			npcInteractionsManager.TriggerNPCInteraction(npcID, npcState);
		}
	}
}
