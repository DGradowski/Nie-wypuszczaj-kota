using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Message
{
	public int actorID;
	[TextArea(3, 20)]
	public string text;
	public string optionA;
	public string optionB;
	public Message[] dialogueA;
	public Message[] dialogueB;
}

[System.Serializable]
public class Actor
{
	public string name;
	public Sprite image;
}