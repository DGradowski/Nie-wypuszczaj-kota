using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionExample : MonoBehaviour
{
	[Header("NPC's")]
	[SerializeField] InteractiveObject buffedRat;

	[Header("Other")]
	[SerializeField] InteractiveObject cheese;

	public void ProcessInteraction(int id, int state, GameObject gameObject = null)
	{
		switch (id)
		{
			case 0:
				BuffedRatInteractions(state);
				break;
			case 1:
				PickUpCheese();
				break;
			case 2:
				DialogueInteraction(state);
				break;

		}
	}

	private void BuffedRatInteractions(int state)
	{

	}

	private void PickUpCheese()
	{

	}

	private void DialogueInteraction(int state)
	{
		switch (state)
		{
			case 0:
				cheese.state = 1;
				buffedRat.state = 1;
				break;
			case 1:
				buffedRat.state = 2;
				break;

		}
	}
}
