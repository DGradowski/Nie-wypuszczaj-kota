using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sentence
{
	public string name;
	public Sprite image;
	[TextArea(3, 20)]
	public string text;
	public string optionA;
	public string optionB;
}