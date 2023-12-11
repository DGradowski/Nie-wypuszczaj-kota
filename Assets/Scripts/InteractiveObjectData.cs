using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InteractiveObjectData", menuName = "ScriptableObjects/InteractiveObjectData", order = 1)]
public class InteractiveObjectData : ScriptableObject
{
	[Header("Interaction Setting")]
	public int interactionGroup = 0;
	public int interactionID = 0;
	public int interactionState = 0;
}
