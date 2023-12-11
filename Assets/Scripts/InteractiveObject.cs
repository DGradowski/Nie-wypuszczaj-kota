using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObject : MonoBehaviour
{
	[Header("Interaction Settings")]
	public InteractiveObjectData interactiveObjectData;
	public int group = 0;
	public int id = 0;
	public int state = 0;

	InteractionManager interactionManager;


	private void Start()
	{
		group = interactiveObjectData.interactionGroup;
		id = interactiveObjectData.interactionID;
		state = interactiveObjectData.interactionState;

		interactionManager = FindAnyObjectByType<InteractionManager>();
	}

	public void TriggerInteraction()
	{

	}

}
