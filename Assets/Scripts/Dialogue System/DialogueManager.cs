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
	[SerializeField] TMP_Text[] optionTexts;
	[SerializeField] Button[] optionButtons;
	[Header("Dialogue Options")]
	[SerializeField] float loadingSpeed;

	DialogueText[] currentDialogue;
	Actor[] currentActors;
	string displayedText = "";
	int dialogueTextID = 0;
	DialogueText message;
	Actor actor;
	float loadedLetters;
	string currentText = "";

	public Dictionary<string, int> condidtions = new Dictionary<string, int>();

	private void Start()
	{
		condidtions.Add("hasCheese", 0);
	}

	public void StartDialogue(DialogueText[] dialogue, Actor[] actors)
	{
		currentDialogue = dialogue;
		currentActors = actors;
		dialoguePanel.SetActive(true);
		dialogueTextID = 0;
		ClearPanel();
	}

	private void ClearPanel()
	{
		message = currentDialogue[dialogueTextID];
		actor = currentActors[message.actorID];
		characterImage.sprite = actor.images[message.actorImageID];
		characterName.text = actor.name;
		currentText = currentDialogue[dialogueTextID].text;
		loadedLetters = 0f;
		displayedText = "";
		optionButtons[0].gameObject.SetActive(false);
		optionButtons[1].gameObject.SetActive(false);
		optionButtons[2].gameObject.SetActive(false);
	}

	private void LoadDialogueText()
	{
		if (loadedLetters < currentText.Length)
		{
			loadedLetters += Time.unscaledDeltaTime * loadingSpeed;
			if (displayedText.Length < loadedLetters)
			{
				if (displayedText.Length == currentText.Length) return;
				displayedText += currentText[displayedText.Length];
			}
			dialogueText.text = displayedText;
		}
	}

	private void DisplayOptions()
	{
		if (message.options.Length == 0) return;
		if (displayedText.Length == currentText.Length)
		{
			int i = 0;
			foreach (DialogueOption option in message.options)
			{
				optionButtons[i].gameObject.SetActive(true);
				optionTexts[i].text = option.text;
				i++;
			}
		}
	}

	private void LoadNextMessage()
	{
		dialogueTextID++;
		ClearPanel();
	}

	private void HandleInputs()
	{
		if (Input.GetKeyDown("space"))
		{
			if (displayedText.Length < currentText.Length)
			{
				loadedLetters = currentText.Length;
				displayedText = currentText;
				dialogueText.text = displayedText;
			}
			else if (dialogueTextID == currentDialogue.Length - 1)
			{
				dialoguePanel.SetActive(false);
			}
			else
			{
				LoadNextMessage();
			}

		}
	}

	private void Update()
	{
		if (!dialoguePanel.active) return;
		HandleInputs();
		LoadDialogueText();
		DisplayOptions();
	}
}
