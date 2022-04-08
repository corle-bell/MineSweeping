using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine;
using InfinityCode.uContext;
using InfinityCode.uContext.Tools;
using InfinityCode.uContext.Windows;

namespace UnityToolbarExtender.Examples
{
	
	[InitializeOnLoad]
	public class ToolBarButton
	{
		static ToolBarButton()
		{
			ToolbarManager.AddRightToolbar("FastRun", OnRightToolbarGUI);
			ToolbarManager.AddRightToolbar("Info", OnToolbarGUI);
		}

		static void OnRightToolbarGUI()
		{
			var tex = EditorGUIUtility.IconContent("d_SurfaceEffector2D Icon").image; 
			if (GUILayoutUtils.Button(new GUIContent(tex, "FastRun"), Styles.appToolbarButtonLeft, GUILayout.Width(30)) == ButtonEvent.click)
			{
				Bm.EditorTool.Scene.SceneWindowEditor.FastRun();
			}
		}

		static void OnToolbarGUI()
		{
			GUILayout.FlexibleSpace();

			GUILayout.Label("当前选中物体数量:" + Selection.gameObjects.Length, Styles.whiteLabel);

		}
	}

	
}