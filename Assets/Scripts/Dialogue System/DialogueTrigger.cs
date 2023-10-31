using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
	public Sentence[] dialogueExample;

	DialogueManager dialogueManager;

	private void Start()
	{
		dialogueManager = FindAnyObjectByType<DialogueManager>();
	}

	public void  triggerDialogue()
	{
		dialogueManager.StartDialogue(dialogueExample);
	}
}
