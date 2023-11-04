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

	Sentence[] currentDialogue;
	string currentText = "";
	int currentId;
	float lettersLoaded;




	public void StartDialogue(Sentence[] dialogue)
	{
		dialoguePanel.SetActive(true);
		currentDialogue = dialogue;
		currentId = 0;
		lettersLoaded = 0;
		currentText = "";
		UpdateNameAndImage();
	}

	public void ContinueDialogue(int option)
	{
		switch (option)
		{
			case 0:
				currentDialogue = currentDialogue[currentId].dialogueA;
				break;
			case 1:
				currentDialogue = currentDialogue[currentId].dialogueB;
				break;
		}
		currentId = 0;
		lettersLoaded = 0;
		currentText = "";
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
		characterName.text = currentDialogue[currentId].name;
		characterImage.sprite = currentDialogue[currentId].image;
	}

	void LoadNextSentence()
	{
		currentId++;
		lettersLoaded = 0f;
		currentText = "";
		if (currentId < currentDialogue.Length)
			UpdateNameAndImage();
		optionAButton.gameObject.SetActive(false);
		optionBButton.gameObject.SetActive(false);
	}
	void ShowOptions()
	{
		if (currentDialogue[currentId].optionA == "")
			return;
		if (currentDialogue[currentId].optionB == "")
			return;
		optionAButton.gameObject.SetActive(true);
		optionBButton.gameObject.SetActive(true);

		optionAText.text = currentDialogue[currentId].optionA;
		optionBText.text = currentDialogue[currentId].optionB;
	}

	private void Update()
	{
		if (currentDialogue == null)
			return;

		if (Input.GetKeyDown("space"))
		{
			if (currentText.Length < currentDialogue[currentId].text.Length)
			{
				currentText = currentDialogue[currentId].text;
				ShowOptions();
			}
			else if (currentDialogue[currentId].optionA == "")
			{
				LoadNextSentence();
			}
		}

		if (currentId >= currentDialogue.Length)
		{
			EndDialogue();
			return;
		}
		if (currentText.Length < currentDialogue[currentId].text.Length)
		{
			lettersLoaded += Time.deltaTime * loadingSpeed;
			if ((int)lettersLoaded >= currentText.Length)
			{
				currentText += currentDialogue[currentId].text[currentText.Length];
			}
			if (currentText.Length >= currentDialogue[currentId].text.Length)
			{
				ShowOptions();
			}
		}
		dialogueText.text = currentText;

	}
}
