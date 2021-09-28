using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace UnityToolbarExtender.Examples
{
	public static class ToolbarStyles
	{
		public static readonly GUIStyle commandLongButtonStyle;
		public static readonly GUIStyle commandButtonStyle;
		public static readonly GUIStyle commandLabelStyle;
		static ToolbarStyles()
		{
			commandButtonStyle = new GUIStyle("Command")
			{
				fontSize = 12,
				alignment = TextAnchor.MiddleCenter,
				imagePosition = ImagePosition.ImageLeft,
				fontStyle = FontStyle.Normal,
			};

			commandLongButtonStyle = new GUIStyle("Command")
			{
				fontSize = 12,
				alignment = TextAnchor.MiddleCenter,
				imagePosition = ImagePosition.ImageAbove,
				fontStyle = FontStyle.Normal,
				fixedWidth = 80
			};

			commandLabelStyle = new GUIStyle("Label")
			{
				fontSize = 12,
				alignment = TextAnchor.MiddleCenter,
				fontStyle = FontStyle.Normal
			};
		}
	}

	[InitializeOnLoad]
	public class ToolBarButton
	{
		static ToolBarButton()
		{
			ToolbarExtender.LeftToolbarGUI.Add(OnToolbarGUI);
			ToolbarExtender.RightToolbarGUI.Add(OnRightToolbarGUI);
		}


		static void OnRightToolbarGUI()
		{
			var tex = EditorGUIUtility.IconContent(@"UnityEditor.SceneView").image;
			if (GUILayout.Button(tex, ToolbarStyles.commandButtonStyle))
			{
                SceneWindowEditor.FastRun();
			}
		}

		static void OnToolbarGUI()
		{
			GUILayout.FlexibleSpace();

			GUILayout.Label("当前选中物体数量:" + Selection.gameObjects.Length, ToolbarStyles.commandLabelStyle);


			if (GUILayout.Button(new GUIContent("Slow", "timeSlow"), ToolbarStyles.commandLongButtonStyle))
			{
				Time.timeScale = 0.1f;
			}

			if(GUILayout.Button(new GUIContent("restore", "timeRestore"), ToolbarStyles.commandLongButtonStyle))
			{
				Time.timeScale = 1f;
			}

			if (GUILayout.Button(new GUIContent("distance", "Distance By Selects"), ToolbarStyles.commandLongButtonStyle))
			{
				if(Selection.transforms.Length==2)
                {
					Debug.Log("距离为: "+Vector3.Distance(Selection.transforms[0].position, Selection.transforms[1].position));
				}
			}

		}
	}

	
}