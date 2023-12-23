using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
	[Header("Interaction Game Objects")]
	[SerializeField] MissionExample missionExample;
    public void UseInteractionSwitch(int group, int id, int state, GameObject gameObject = null)
	{
		switch (group)
		{
			case 0:
				missionExample.ProcessInteraction(id, state, gameObject);
				break;
			case 1:
				// In progress
				break;
			case 4:
				gameObject.GetComponent<Portal>().TeleportPlayer();
				break;
			default:
				// Same here
				break;
		}
	}
}
