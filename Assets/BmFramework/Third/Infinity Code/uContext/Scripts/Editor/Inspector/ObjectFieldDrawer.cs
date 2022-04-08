﻿/*           INFINITY CODE          */
/*     https://infinity-code.com    */

using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using Object = UnityEngine.Object;

namespace InfinityCode.uContext.Inspector
{
    [CustomPropertyDrawer(typeof(Object), true)]
    public class ObjectFieldDrawer : PropertyDrawer
    {
        public delegate void GUIDelegate(Rect area, SerializedProperty property, GUIContent label);

        public static GUIDelegate OnGUIBefore;
        public static GUIDelegate OnGUIAfter;

        public override void OnGUI(Rect area, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType != SerializedPropertyType.ObjectReference || property.hasMultipleDifferentValues || property.objectReferenceValue == null)
            {
                EditorGUI.PropertyField(area, property, label);
                return;
            }

            DelegateHelper.InvokeGUI(OnGUIBefore, area, property, label);
            EditorGUI.PropertyField(area, property, label, property.isExpanded);
            DelegateHelper.InvokeGUI(OnGUIAfter, area, property, label);
        }
    }
}