using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlayerPrefsWindow : EditorWindow
{
    [MenuItem("Tools/存档预览/打开", false)]
    static void Open()
    {
        EditorWindow.GetWindow<PlayerPrefsWindow>(false, "存档预览", true).Show();
    }

    List<PlayerPrefPair> data = new List<PlayerPrefPair>();
    string[] valueName = new string[] { "int", "float", "string" };
    string addKey;
    string addValue;
    int addId;
    Vector2 scroll;
    private void OnGUI()
    {
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("data");

        EditorGUILayout.BeginHorizontal();
        addKey = EditorGUILayout.TextField("Key", addKey);
        addId = EditorGUILayout.Popup(addId, valueName);
        addValue = EditorGUILayout.TextField("Value", addValue);

        if (GUILayout.Button("add"))
        {            
            switch (addId)
            {
                case 0:
                    PlayerPrefs.SetInt(addKey, int.Parse(addValue));
                    break;
                case 1:
                    PlayerPrefs.SetFloat(addKey, float.Parse(addValue));
                    break;
                case 2:
                    PlayerPrefs.SetString(addKey, addValue);
                    break;
            }
            GetAll();
        }
        if (GUILayout.Button("refresh"))
        {
            GetAll();
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();
        scroll = EditorGUILayout.BeginScrollView(scroll);

        PlayerPrefPair delData=null;
        
        foreach (var item in data)
        {
            EditorGUILayout.BeginHorizontal();
            var tmp = EditorGUILayout.TextField("Key:", item.Key);
            if (item.Value.GetType() == typeof(int))
            {
               
                item.Value = EditorGUILayout.IntField("Value:", (int)item.Value);
                if (GUILayout.Button("Save"))
                {
                    PlayerPrefs.SetInt(item.Key, (int)item.Value);
                }
            }
            if (item.Value.GetType() == typeof(float))
            {
                item.Value = EditorGUILayout.FloatField("Value:", (float)item.Value);
                if (GUILayout.Button("Save"))
                {
                    PlayerPrefs.SetFloat(item.Key, (float)item.Value);
                }
            }
            if (item.Value.GetType() == typeof(string))
            {
                item.Value = EditorGUILayout.TextField("Value:", (string)item.Value);
                if (GUILayout.Button("Save"))
                {
                    PlayerPrefs.SetString(item.Key, (string)item.Value);
                }
            }

            
            if (GUILayout.Button("Del"))
            {
                PlayerPrefs.DeleteKey(item.Key);
                delData = item;
            }

            EditorGUILayout.EndHorizontal();
        }

        if(delData!=null)
        {
            data.Remove(delData);
        }
        EditorGUILayout.EndScrollView();

    }

    private void OnEnable()
    {
       GetAll();
    }

    public void GetAll()
    {
        data.Clear();
        data.AddRange(PlayerPrefsExtension.GetAll());
    }
}
