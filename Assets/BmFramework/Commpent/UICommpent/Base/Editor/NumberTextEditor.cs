using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;


[CustomEditor(typeof(NumberText))]
public class NumberTextEditor : Editor {
	public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
	}
}



