using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
	[Header("UI Components")]
	[SerializeField] GameObject dialoguePanel;
	[SerializeField] TMP_Text characterName;
	[SerializeField] Image characterImage;
	[SerializeField] TMP_Text dialogueText;
	[SerializeField] Button optionAButton;
	[SerializeField] TMP_Text optionAText;
	[SerializeField] Button optionBButton;
	[SerializeField] TMP_Text optionBText;
	

	[Header("Dialogue Options")]
	[SerializeField] float loadingSpeed;

	Message[] currentDialogue;
	Actor[] currentActors;
	string displayedText = "";
	int activeMassageID;
	Message message;
	Actor actor;
	float lettersLoaded;


	public void StartDialogue(Message[] dialogue, Actor[] actors)
	{
		dialoguePanel.SetActive(true);
		currentDialogue = dialogue;
		currentActors = actors;
		activeMassageID = 0;
		message = currentDialogue[activeMassageID];
		actor = currentActors[message.actorID];
		lettersLoaded = 0;
		displayedText = "";
		UpdateNameAndImage();
	}

	public void ContinueDialogue(int option)
	{
		switch (option)
		{
			case 0:
				currentDialogue = message.dialogueA;
				break;
			case 1:
				currentDialogue = message.dialogueB;
				break;
		}
		activeMassageID = 0;
		message = currentDialogue[activeMassageID];
		actor = currentActors[message.actorID];
		lettersLoaded = 0;
		displayedText = "";
		UpdateNameAndImage();
		optionAButton.gameObject.SetActive(false);
		optionBButton.gameObject.SetActive(false);
	}

	void EndDialogue()
	{
		dialoguePanel.SetActive(false);
		currentDialogue = null;
	}

	void UpdateNameAndImage()
	{
		characterName.text = actor.name;
		characterImage.sprite = actor.image;
	}

	void LoadNextSentence()
	{
		activeMassageID++;
		if (activeMassageID < currentDialogue.Length)
		{
			lettersLoaded = 0f;
			displayedText = "";
			message = currentDialogue[activeMassageID];
			actor = currentActors[message.actorID];
			UpdateNameAndImage();
		}
		optionAButton.gameObject.SetActive(false);
		optionBButton.gameObject.SetActive(false);
	}
	void ShowOptions()
	{
		if (message.optionA != "")
		{
			optionAButton.gameObject.SetActive(true);
			optionAText.text = message.optionA;
		}

		if (message.optionB != "")
		{
			optionBButton.gameObject.SetActive(true);
			optionBText.text = message.optionB;
		}
	}

	private void Update()
	{
		if (currentDialogue == null)
			return;

		if (Input.GetKeyDown("space"))
		{
			if (displayedText.Length < message.text.Length)
			{
				displayedText = message.text;
				ShowOptions();
			}
			else if (message.optionA == "")
			{
				LoadNextSentence();
			}
		}

		if (activeMassageID >= currentDialogue.Length)
		{
			EndDialogue();
			return;
		}
		if (displayedText.Length < message.text.Length)
		{
			lettersLoaded += Time.deltaTime * loadingSpeed;
			if ((int)lettersLoaded >= displayedText.Length)
			{
				displayedText += message.text[displayedText.Length];
			}	
			if (displayedText.Length >= message.text.Length)
			{
				ShowOptions();
			}
		}
		dialogueText.text = displayedText;

	}
}
