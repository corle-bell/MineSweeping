using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
namespace BmFramework.Core
{
    public class EditorMessgeBox : EditorWindow
    {
        public string content = "";
        public System.Action<bool> callback;
        public MessageType msgType;
        public string[] buttonsName;
        private GUIStyle m_tempFontStyle = new GUIStyle();

        public static void Open(string _title, string _conent, string buttonName, MessageType type=MessageType.Info)
        {
            Open(_title, _conent, buttonName, new Vector2(300, 240), type);
        }

        public static void Open(string _title, string _conent, string leftButton, string rightButton, System.Action<bool> _callback = null, MessageType type = MessageType.None)
        {
            var window = GetWindow<EditorMessgeBox>(_title);
            window.maxSize = new Vector2(300, 240);
            window.minSize = new Vector2(300, 240);
            window.Show();
            window.content = _conent;
            window.callback = _callback;
            window.msgType = type;

            window.OnCreate();

            window.buttonsName = new string[] { leftButton, rightButton };
        }

        public static void Open(string _title, string _conent, string buttonName, Vector2 size, MessageType type = MessageType.None)
        {
            var window = GetWindow<EditorMessgeBox>(_title);
            window.maxSize = size;
            window.minSize = size;
            window.msgType = type;
            window.Show();
            window.content = _conent;

            window.OnCreate();

            window.buttonsName = new string[] { buttonName };
        }

        public void OnCreate()
        {
            switch(this.msgType)
            {
                case MessageType.None:
                    m_tempFontStyle.normal.textColor = Color.white;
                    break;
                case MessageType.Info:
                    m_tempFontStyle.normal.textColor = Color.green;
                    break;
                case MessageType.Error:
                    m_tempFontStyle.normal.textColor = Color.red;
                    break;
                case MessageType.Warning:
                    m_tempFontStyle.normal.textColor = Color.yellow;
                    break;
            }

            m_tempFontStyle.fontStyle = FontStyle.Normal;
            m_tempFontStyle.fontSize = 13;
        }

        protected void OnGUI()
        {
            GUILayout.Space(10);
            EditorGUILayout.TextArea(content, m_tempFontStyle, GUILayout.MinHeight(position.height - 30));
            switch (buttonsName.Length)
            {
                case 1:
                    if(GUILayout.Button(buttonsName[0]))
                    {
                        this.Close();
                    }
                    break;
                case 2:
                    GUILayout.BeginHorizontal();
                    if (GUILayout.Button(buttonsName[0]))
                    {
                        this.Close();
                        callback?.Invoke(true);
                        callback = null;
                    }
                    if (GUILayout.Button(buttonsName[1]))
                    {
                        this.Close();
                        callback?.Invoke(false);
                        callback = null;
                    }
                    GUILayout.EndHorizontal();
                    break;
            }
        }


    }
}
