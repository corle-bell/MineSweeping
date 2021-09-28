using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
namespace Bm.Lerp
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(BmLerpProperty_Float), true)]
    public class BmLerpProperty_FloatEditor : BmLerpBaseEditor
    {
        protected SerializedProperty PropertyNameProperty;
        protected SerializedProperty TargetProperty;
        public void OnEnable()
        {
            PropertyNameProperty = serializedObject.FindProperty("Property");
            TargetProperty = serializedObject.FindProperty("component");
        }

   
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            /*var data = target as BmLerpProperty_Float;

            EditorUtil.ComponentTreeMenu<Component>("component", TargetProperty, data.transform);

            var type = data.component.GetType();
            EditorUtil.PropertyTreeMenu("Dst Property", type, PropertyNameProperty);

            base.OnInspectorGUI();

            if (GUILayout.Button("Bake"))
            {
                data.Init();
            }*/
        }
    }

}
