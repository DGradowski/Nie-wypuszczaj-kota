using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InteractiveObjectData", menuName = "ScriptableObjects/InteractiveObjectData", order = 1)]
public class InteractiveObjectData : ScriptableObject
{
	[Header("Interaction Setting")]
	public int group = 0;
	public int ID = 0;
	public int state = 0;
}
