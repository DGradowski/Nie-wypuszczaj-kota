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

	

	private void Update()
	{
		if (currentDialogue == null)
			return;

		if (Input.GetKeyDown("space"))
		{
			if (currentText.Length < currentDialogue[currentId].text.Length)
			{
				currentText = currentDialogue[currentId].text;
			}
			else
			{
				currentId++;
				lettersLoaded = 0f;
				currentText = "";
				if (currentId < currentDialogue.Length)
					UpdateNameAndImage();
			}
		}

		if (currentId >= currentDialogue.Length)
		{
			EndDialogue();
		}
		else if (currentText.Length < currentDialogue[currentId].text.Length)
		{
			lettersLoaded += Time.deltaTime * loadingSpeed;
			if ((int)lettersLoaded >= currentText.Length)
			{
				currentText += currentDialogue[currentId].text[currentText.Length];
			}
		}
		dialogueText.text = currentText;

	}
}
