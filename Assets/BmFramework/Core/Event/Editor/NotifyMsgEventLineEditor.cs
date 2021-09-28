using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEditor;

namespace BmFramework.Core
{
	[CustomEditor(typeof(NotifyMsgEventLine))]
	public class NotifyMsgEventLineEditor: Editor
	{
        NotifyMsgEventLine script;        
        private void OnEnable()
        {
            script = target as NotifyMsgEventLine;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if(GUILayout.Button("添加事件"))
            {
                script.AddEventByTime("", script.time, null);
                EditorUtility.SetDirty(script);
            }

            script.time = EditorGUILayout.Slider(script.time, 0, script.duration);

            GUI.backgroundColor = Color.white;
            Rect progressRect = GUILayoutUtility.GetRect(50, 20 * (script.events.Count + 1));
            EditorGUI.DrawRect(progressRect, new Color(0.15f, 0.15f, 0.15f));


            float buttonLen = 40;
            progressRect.width -= buttonLen*2;

            Rect groupRect = new Rect(progressRect);
            groupRect.height = 20;
            EditorGUI.ProgressBar(groupRect, script.percent, string.Format("{0} ( {1:f}S ) : 进度:{2:f} 时间:{3:f}", "Director", script.duration, script.percent, script.percent * script.duration));


            for (int i = 0; i < script.events.Count; i++)
            {
                
                var node = script.events[i];
                GUI.backgroundColor = node.isDo?Color.red:Color.green;

                //绘制节点进度条
                Rect t1 = new Rect(progressRect);
                t1.y += 20 * (i + 1);
                t1.height = 20;
                t1.width = progressRect.width;
                t1.x = progressRect.x;

                EditorGUI.ProgressBar(t1, node.progress, string.Format("{0} : 进度:{1:f3} 时间:{2:f3}",node.name, node.progress, node.progress*script.duration));

                Rect line = new Rect(t1);
                line.width = progressRect.width;
                line.x = progressRect.x;
                line.height = 20;
                GUI.backgroundColor = i % 2 == 0 ? Color.white : Color.cyan;
                GUI.Box(line, "");

                //绘制节点进度条左值滑动帧
                Rect t2 = new Rect(t1);
                t2.x = t1.width * node.progress+t1.xMin;
                t2.width = 6;

                MouseCheck(node, t2, i, t1);
                EditorGUI.DrawRect(t2, Color.green);
                EditorGUI.DrawTextureAlpha(t2, EditorGUIUtility.IconContent("mini btn on@2x").image);
                

                //绘制节点功能键
                GUI.backgroundColor = Color.red;
                GUI.contentColor = Color.white;
                t1.width = buttonLen;
                t1.x = progressRect.width + t1.width / 2 + 5;

                if (GUI.Button(t1, "Edit"))
                {
                    NotifyMsgEventLineNodeWindow.Open(script, i, (float)script.duration);
                }

                t1.width = buttonLen;
                t1.x = progressRect.width + t1.width / 2 + 5+buttonLen;
                if (GUI.Button(t1, "Del"))
                {
                    script.events.Remove(node);
                }
            }

            //指针




            if(!Application.isPlaying)
            {
                script.percent = script.time / script.duration;
                progressRect.x += progressRect.width * script.percent;
                progressRect.width = 1;
                EditorGUI.DrawRect(progressRect, Color.white);
            }
        }

        #region Drag Frame
        Vector2 touchBegin;
        float startX;
        int selectId = 0;
        void MouseCheck(NotifyMsgEventLineNode node, Rect _rect, int _id, Rect _max)
        {
            Event aEvent;
            aEvent = Event.current;
            switch (aEvent.type)
            {
                case EventType.MouseDown:
                    if (_rect.Contains(aEvent.mousePosition))
                    {
                        selectId = _id;
                        touchBegin = aEvent.mousePosition;
                        startX = _rect.xMin;
                    }
                    else
                    {
                        if (_max.Contains(aEvent.mousePosition))
                        {
                            node.progress = (aEvent.mousePosition.x - _max.xMin) / _max.width;
                            Repaint();
                        }
                    }
                    break;
                case EventType.MouseUp:
                    selectId = -1;
                    break;
                case EventType.MouseDrag:
                    if (!_max.Contains(aEvent.mousePosition))
                    {
                        return;
                    }
                    if (_id == selectId)
                    {
                        var t = aEvent.mousePosition - touchBegin;
                        float a = (startX + t.x) - _max.xMin;
                        node.progress = node.progress < 0.5f? node.progress = a / _max.width:(startX + t.x) / _max.width;
                        node.progress = Mathf.Clamp(node.progress, 0, 1);


                        Repaint();
                    }

                    break;
                default:
                    break;
            }
        }
        #endregion
    }
}
