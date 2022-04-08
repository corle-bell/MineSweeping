using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace BmFramework.Core
{
    public interface IManagerBaseNode
    {
        string name { get; set; }
        string asset_path { get; set; }
    }
    
    public class ManagerBaseEdtior : EditorWindow
    {
        public List<IManagerBaseNode> nodeList = new List<IManagerBaseNode>();
        protected Vector2 scroll;

        List<string> btnName = new List<string>();
        List<System.Action<int>> btnFuns = new List<System.Action<int>>();
        List<int> btnFlags = new List<int>();

        protected void AddButton(string _name, System.Action<int> _action, int _flag=0)
        {
            btnName.Add(_name);
            btnFuns.Add(_action);
            btnFlags.Add(_flag);
        }

        protected void ClearButton()
        {
            btnName.Clear();
            btnFuns.Clear();
        }

        protected virtual void Scan()
        {

        }

        protected virtual void DrawList()
        {
            if(GUILayout.Button("Scan"))
            {
                Scan();
            }

            scroll = EditorGUILayout.BeginScrollView(scroll);
            for(int i=0; i<nodeList.Count; i++)
            {
                var item = nodeList[i];
                EditorGUILayout.BeginHorizontal();

                EditorGUILayout.LabelField("名称", item.name, EditorStyles.textArea);

                for(int j=0; j<btnName.Count; j++)
                {
                    DrawButton(btnName[j], btnFuns[j], i);
                }

                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndScrollView();
        }

        protected void DrawButton(string _name, System.Action<int> _action, int _id)
        {
            if(GUILayout.Button(_name))
            {
                _action.Invoke(_id);
            }
        }

    }
}
