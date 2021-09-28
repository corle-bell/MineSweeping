using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
public class GUIStyleViewer : EditorWindow
{
    private Vector2 scrollVector2 = Vector2.zero;
    private string search = "";

    [MenuItem("编辑器/GUIStyle查看器")]
    public static void InitWindow()
    {
        EditorWindow.GetWindow(typeof(GUIStyleViewer));
    }

    void OnGUI()
    {
        GUILayout.BeginHorizontal("HelpBox");
        GUILayout.Space(30);
        search = EditorGUILayout.TextField("", search, "SearchTextField", GUILayout.MaxWidth(position.x / 3));
        GUILayout.Label("", "SearchCancelButtonEmpty");
        GUILayout.EndHorizontal();
        scrollVector2 = GUILayout.BeginScrollView(scrollVector2);
        
        for(int i=0; i<GUI.skin.customStyles.Length; i++)
        {
            GUIStyle style = GUI.skin.customStyles[i];
            if (style.name.ToLower().Contains(search.ToLower()))
            {
                DrawStyleItem(style, i);
            }
        }
        GUILayout.EndScrollView();
    }

    void DrawStyleItem(GUIStyle style, int index)
    {
        GUILayout.BeginHorizontal("box");
        GUILayout.Space(40);
        EditorGUILayout.SelectableLabel(style.name);
        GUILayout.FlexibleSpace();
         GUILayout.Space(40);
        EditorGUILayout.SelectableLabel("序号: "+index);
        GUILayout.FlexibleSpace();
        EditorGUILayout.SelectableLabel(style.name, style);
        GUILayout.Space(40);
        EditorGUILayout.SelectableLabel("", style, GUILayout.Height(40), GUILayout.Width(40));
        GUILayout.Space(50);
        if (GUILayout.Button("复制到剪贴板"))
        {
            TextEditor textEditor = new TextEditor();
            textEditor.text = ""+index;
            textEditor.OnFocus();
            textEditor.Copy();
        }
        GUILayout.EndHorizontal();
        GUILayout.Space(10);
    }
}

#endif