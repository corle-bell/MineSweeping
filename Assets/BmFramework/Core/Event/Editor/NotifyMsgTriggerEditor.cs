using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace BmFramework.Core
{
    [CustomEditor(typeof(NotifyMsgTrigger))]
    public class NotifyMsgTriggerEditor : Editor
    {
        SerializedObject eventObject;
        NotifyMsgTrigger notifyMsgTrigger;
        string keyAdd = "";
        private void OnEnable()
        {
            notifyMsgTrigger = target as NotifyMsgTrigger;
            eventObject = new SerializedObject(target);
        }
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();



            SerializedProperty startEvent = eventObject.FindProperty("startEvent");
            EditorGUILayout.PropertyField(startEvent, new GUIContent("Start事件"));

            GUILayout.BeginHorizontal();
            keyAdd = EditorGUILayout.TextField("事件:", keyAdd);
            if (GUILayout.Button("添加事件"))
            {
                var t = new NotifyMsgTriggerData();
                t.key = keyAdd;
                notifyMsgTrigger.eventArr.Add(t);

                EditorUtility.SetDirty(target);
            }
            GUILayout.EndHorizontal();

        }
    }
}

