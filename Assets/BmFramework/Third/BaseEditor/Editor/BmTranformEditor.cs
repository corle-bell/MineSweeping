using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Text.RegularExpressions;

namespace UnityEditor
{
    public class BmTranformEditor
    {
        [MenuItem("CONTEXT/Transform/Copy Tranform Data")]
        public static void CopyData(MenuCommand cmd)
        {
            Transform trans = cmd.context as Transform;
            if (trans == null) return;
            string tmp = "";

            tmp += GetStringFromVector3(trans.localPosition);
            tmp += "#";
            tmp += GetStringFromVector3(trans.localEulerAngles);
            tmp += "#";
            tmp += GetStringFromVector3(trans.localScale);

            ToClipBoard(tmp);
        }

        [MenuItem("CONTEXT/Transform/Paste Tranform Data")]
        public static void PasteData(MenuCommand cmd)
        {
            Transform trans = cmd.context as Transform;
            if (trans == null) return;
            string tmp = GetStringFromClipBoard();
            string[] arr = tmp.Split(new char[] { '#' });

            if(arr.Length!=3)
            {
                Debug.LogError("格式错误!" + tmp);
                return;
            }

            Undo.RecordObject(trans, "Trans");

            trans.localPosition = stringToVector3(arr[0]);
            trans.localEulerAngles = stringToVector3(arr[1]);
            trans.localScale = stringToVector3(arr[2]); 
        }

        [MenuItem("CONTEXT/Transform/Copy WorldPosition")]
        public static void CopyWorldPosition(MenuCommand cmd)
        {
            Transform trans = cmd.context as Transform;
            if (trans == null) return;
            string tmp = "";

            tmp += GetStringFromVector3(trans.position);
            tmp += "#";
            tmp += GetStringFromVector3(trans.eulerAngles);

            ToClipBoard(tmp);
        }

        [MenuItem("CONTEXT/Transform/Paste WorldPosition")]
        public static void PasteWorldPosition(MenuCommand cmd)
        {
            Transform trans = cmd.context as Transform;
            if (trans == null) return;
            string tmp = GetStringFromClipBoard();
            string[] arr = tmp.Split(new char[] { '#' });

            if (arr.Length != 2)
            {
                Debug.LogError("格式错误!" + tmp);
                return;
            }

            Undo.RecordObject(trans, "Trans");

            trans.position = stringToVector3(arr[0]);
            trans.eulerAngles = stringToVector3(arr[1]);
        }

        [MenuItem("GameObject/Transform/SwapTransfrom")]
        public static void SwapTranform()
        {
            var trans = Selection.transforms;
            if(trans.Length!=2)
            {
                return;
            }

            Undo.RecordObject(trans[0], "Trans0");
            Undo.RecordObject(trans[1], "Trans1");

            var p = trans[0].position;
            var r = trans[0].rotation;
            var s = trans[0].localScale;

            trans[0].position = trans[1].position;
            trans[0].rotation = trans[1].rotation;
            trans[0].localScale = trans[1].localScale;


            trans[1].position = p;
            trans[1].rotation = r;
            trans[1].localScale = s;

            
        }

        static string GetStringFromVector3(Vector3 _src)
        {
            return string.Format("({0}, {1}, {2})", _src.x, _src.y, _src.z);
        }

        static void ToClipBoard(string _data)
        {
            TextEditor te = new TextEditor();
            te.text = _data;
            te.SelectAll();
            te.Copy();
        }

        static string GetStringFromClipBoard()
        {
            string str = GUIUtility.systemCopyBuffer;
            return str;
        }

        static Vector3 stringToVector3(string _text)
        {
            Vector3 zero = Vector3.zero;
            MatchCollection matchCollection = Regex.Matches(_text, "(?<=\\().*?(?=\\))");
            string value = matchCollection[0].Value;
            string[] array = Regex.Split(value, ",");
            zero.x = float.Parse(array[0]);
            zero.y = float.Parse(array[1]);
            zero.z = float.Parse(array[2]);
            return zero;
        }
    }
}

