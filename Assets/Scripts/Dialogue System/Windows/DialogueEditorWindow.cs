using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace DSWindows
{
	public class DialogueEditorWindow : EditorWindow
	{
		[MenuItem("Window/DialogueSystem/DialogueGraph")]
		public static void Open()
		{
			GetWindow<DialogueEditorWindow>("Dialogue Graph");
		}

		private void OnEnable()
		{
			AddGraphView();
		}

		private void AddGraphView()
		{
			DialogueGraphView graphView = new DialogueGraphView();

			graphView.StretchToParentSize();

			rootVisualElement.Add(graphView);
		}
	}
}
