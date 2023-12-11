using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionExample : MonoBehaviour
{
	[Header("NPC's")]
	[SerializeField] InteractiveObject buffedRat;

	[Header("Other")]
	[SerializeField] InteractiveObject cheese;
	private DialogueManager dialogueManager;
	public Actor[] actors;

	public DialogueText[] dialogue1;
	public DialogueText[] dialogue2;
	public DialogueText[] dialogue3;

	private void Start()
	{
		dialogueManager = FindAnyObjectByType<DialogueManager>();
	}

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
		switch (state)
		{
			case 0:
				dialogueManager.StartDialogue(dialogue1, actors);
				break;
			case 1:
				dialogueManager.StartDialogue(dialogue2, actors);
				break;
			case 2:
				dialogueManager.StartDialogue(dialogue3, actors);
				break;
		}
	}

	private void PickUpCheese()
	{
		if (cheese.state == 1)
		{
			cheese.gameObject.SetActive(false);
			dialogueManager.ChangeCondition("hasCheese", 1);
		}
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
