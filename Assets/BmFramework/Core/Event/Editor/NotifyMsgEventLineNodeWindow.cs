using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using UnityEditorInternal;
using UnityEngine.Events;

namespace BmFramework.Core
{
   
    public class NotifyMsgEventLineNodeWindow : EditorWindow
    {
        public NotifyMsgEventLineNode node;
        public int nodeid;
        public NotifyMsgEventLine target;
        public float duration;
        public float time;

        SerializedObject serializedObject;
        SerializedProperty serializedProperty;


        public void Init()
        {
            serializedObject = new SerializedObject(target);
            serializedProperty = serializedObject.FindProperty("events");
            serializedProperty = serializedProperty.GetArrayElementAtIndex(nodeid);
            node = target.events[nodeid];
        }


        private void OnGUI()
        {
            EditorGUILayout.Space();

            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedProperty.FindPropertyRelative("name"));
            EditorGUILayout.PropertyField(serializedProperty.FindPropertyRelative("progress"));

            EditorGUILayout.PropertyField(serializedProperty.FindPropertyRelative("cmd"));
            EditorGUILayout.PropertyField(serializedProperty.FindPropertyRelative("sender"));
            EditorGUILayout.PropertyField(serializedProperty.FindPropertyRelative("SubCmd"));

            EditorGUILayout.PropertyField(serializedProperty.FindPropertyRelative("mEvent"));
            serializedObject.ApplyModifiedProperties();

            time = node.progress * duration;
            float _a = time;
            time = EditorGUILayout.FloatField("执行事件的事件节点(s):", time);
            if(_a!=time)
            {
                node.progress = time / duration;
            }
            if (GUILayout.Button("保存"))
            {
                this.Close();
            }
        }

        public static void Open(NotifyMsgEventLine target, int _id, float _duration)
        {
            NotifyMsgEventLineNodeWindow window = EditorWindow.GetWindow(typeof(NotifyMsgEventLineNodeWindow)) as NotifyMsgEventLineNodeWindow;
            window.duration = _duration;
            window.nodeid = _id;
            window.target = target;
            window.Init();
        }
    }
}
