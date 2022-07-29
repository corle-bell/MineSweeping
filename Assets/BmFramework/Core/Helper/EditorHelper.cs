using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace BmFramework.Core
{
#if UNITY_EDITOR
    [InitializeOnLoadAttribute]
    public static class EditorHelper
    {
        public static string script_init;
        public static string script_title;

        //初始化类时,注册事件处理函数
        static EditorHelper()
        {
            Debug.Log("EditorHelper Create!");
            ReadConfig();
        }

        public static void ReadConfig()
        {
            //读取NewBehaviourScript 模板
            string path = Application.dataPath + "/BmFramework/Templete/Script/NewBehaviourScript.script";
            script_init = FileHandle.instance.ReadAllString(path);

            path = Application.dataPath + "/BmFramework/Templete/Script/ScriptTitle.script";
            script_title = FileHandle.instance.ReadAllString(path);
        }
    }
#endif
}

