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
		interactionManager = FindAnyObjectByType<InteractionManager>();

		if (interactiveObjectData == null) return;
		group = interactiveObjectData.group;
		id = interactiveObjectData.ID;
		state = interactiveObjectData.state;
	}

	public void TriggerInteraction()
	{
		interactionManager.UseInteractionSwitch(group, id, state, gameObject);
	}

}
