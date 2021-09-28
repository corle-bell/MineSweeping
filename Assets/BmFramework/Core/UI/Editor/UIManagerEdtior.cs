using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace BmFramework.Core
{
    [CustomEditor(typeof(UIManager))]
    public class UIManagerEditor:Editor
    {
        public UIManager manager;

        private void OnEnable()
        {
            manager = target as UIManager;
        }

        Vector2 scroll;
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            scroll = EditorGUILayout.BeginScrollView(scroll);
            Rect progressRect = GUILayoutUtility.GetRect(50, 20);
            for (int i=0; i<manager.uiList.Count; i++)
            {
                progressRect.y += 20;
                EditorGUI.ProgressBar(progressRect, 1.0f, manager.uiList[i].ToString());
            }
            EditorGUILayout.EndScrollView();
        }
    }
}
