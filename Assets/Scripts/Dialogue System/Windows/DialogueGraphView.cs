using System;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace DSWindows
{
	public class DialogueGraphView : GraphView
	{
		public DialogueGraphView()
		{
			AddGridBackground();

			AddStyles();
		}

		private void AddStyles()
		{
			StyleSheet styleSheet = (StyleSheet) EditorGUIUtility.Load("Dialogue System/DialogueGraphView.uss");

			styleSheets.Add(styleSheet);
		}

		private void AddGridBackground()
		{
			GridBackground gridBackground = new GridBackground();

			gridBackground.StretchToParentSize();

			Insert(0, gridBackground);
		}
	}
}