using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DialogueTrigger : MonoBehaviour
{
	public DialogueText[] dialogueExample;
	public Actor[] actors;

	DialogueManager dialogueManager;

	private void Start()
	{
		dialogueManager = FindAnyObjectByType<DialogueManager>();
	}

	public void triggerDialogue()
	{
		dialogueManager.StartDialogue(dialogueExample, actors);
	}
}
