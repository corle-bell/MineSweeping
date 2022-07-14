using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Bm.Lerp
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(BmLerpSkinmeshRenderBlendShape), true)]
    public class BmLerpSkinmeshRenderBlendShapeEdtor : Editor
    {
        bool isPreview;

        private SerializedProperty tempProperty;
        private UnityEngine.Object tempObj = null;
        private int selectId = -1;

        private const int Dir_Left = 1;
        private const int Dir_Right = 2;
        private float percent;

        private bool isOrder;

        private string[] blendShapeName;

        private void InitBlendShapeInfo()
        {
            var group = target as BmLerpSkinmeshRenderBlendShape;
            SkinnedMeshRenderer renderer = group.skinnedMesh;

            if(renderer!=null)
            {
                int blendShapeCount = renderer.sharedMesh.blendShapeCount;
                Mesh mesh = renderer.sharedMesh;
                blendShapeName = new string[blendShapeCount];
                for (int i = 0; i < blendShapeName.Length; i++)
                {
                    blendShapeName[i] = mesh.GetBlendShapeName(i);
                }
            }
            else
            {
                blendShapeName = new string[30];
                for(int i=0; i< blendShapeName.Length; i++)
                {
                    blendShapeName[i] = $"Missing Name Id={i}";
                }
            }
            
        }

        public override void OnInspectorGUI()
        {


            var group = target as BmLerpSkinmeshRenderBlendShape;

            EditorGUILayout.BeginHorizontal();
            if (isPreview)
            {
                if (GUILayout.Button("�ر�Ԥ��"))
                {
                    UnPreview();
                }
            }
            else
            {
                if (GUILayout.Button("Ԥ��"))
                {
                    Preview();
                }
            }
            EditorGUILayout.EndHorizontal();



            if (isPreview)
            {
                percent = EditorGUILayout.Slider(percent, 0, 1);

                group.Lerp(percent);
            }

            base.OnInspectorGUI();


            GUI.backgroundColor = Color.green;
            GetDrag();

            GUI.backgroundColor = Color.white;
            Rect progressRect = GUILayoutUtility.GetRect(50, 20 * (group.groupNode.Count + 1));
            EditorGUI.DrawRect(progressRect, new Color(0.15f, 0.15f, 0.15f));


            float buttonLen = 40;
            progressRect.width -= buttonLen * 2;

            Rect groupRect = new Rect(progressRect);
            groupRect.height = 20;
            EditorGUI.ProgressBar(groupRect, group.percent, "Group");

            for (int i = 0; i < group.groupNode.Count; i++)
            {
                GUI.backgroundColor = Color.green;
                var node = group.groupNode[i];

                var pp = group.skinnedMesh==null?1f:group.skinnedMesh.GetBlendShapeWeight(node.Id) * 0.01f;

                //���ƽڵ������
                Rect t1 = new Rect(progressRect);
                t1.y += 20 * (i + 1);
                t1.height = 20;
                t1.width = (node.maxInGroup - node.minInGroup) * progressRect.width;
                t1.x = progressRect.x + progressRect.width * node.minInGroup;

                EditorGUI.ProgressBar(t1, pp, blendShapeName[node.Id]);

                Rect line = new Rect(t1);
                line.width = progressRect.width;
                line.x = progressRect.x;
                line.height = 20;
                GUI.backgroundColor = i % 2 == 0 ? Color.white : Color.cyan;
                GUI.Box(line, "");

                //���ƽڵ��������ֵ����֡
                Rect t2 = new Rect(t1);
                t2.width = 5;

                MouseCheck(node, t2, i, progressRect, Dir_Left);
                EditorGUI.DrawRect(t2, Color.green);

                //���ƽڵ��������ֵ����֡
                t2.x = t1.xMin + t1.width;
                t2.width = 5;
                MouseCheck(node, t2, i, progressRect, Dir_Right);
                EditorGUI.DrawRect(t2, Color.red);

                //���ƽڵ㹦�ܼ�
                GUI.backgroundColor = Color.red;
                GUI.contentColor = Color.white;
                t1.width = buttonLen;
                t1.x = progressRect.width + t1.width / 2 + 5;

                if (GUI.Button(t1, "Edit"))
                {
                    BmLerpBlendShapeNodeWindow.Open(node, blendShapeName);
                }

                t1.width = buttonLen;
                t1.x = progressRect.width + t1.width / 2 + 5 + buttonLen;
                if (GUI.Button(t1, "Del"))
                {
                    group.groupNode.Remove(node);
                }
            }

            //ָ��            
            progressRect.x += progressRect.width * group.percent;
            progressRect.width = 1;
            EditorGUI.DrawRect(progressRect, Color.white);

            //�������й���
            GUI.backgroundColor = Color.cyan;

            group.start_time = EditorGUILayout.Slider("��ʼλ��", group.start_time, 0, 1);
            group.space_time = EditorGUILayout.Slider("�������", group.space_time, -1, 1);
            group.node_len_time = EditorGUILayout.Slider("�����ڵ㳤��", group.node_len_time, -1, 1);
            isOrder = EditorGUILayout.Toggle("��������", isOrder);

            if (isOrder)
            {
                OrderNodes();
            }
        }

        bool isContain(BmLerpBlendShapeNode _lerp)
        {
            var group = target as BmLerpSkinmeshRenderBlendShape;
            foreach (var item in group.groupNode)
            {
                if (item.Id == _lerp.Id)
                {
                    return true;
                }
            }
            return false;
        }

        void OrderNodes()
        {
            var group = target as BmLerpSkinmeshRenderBlendShape;
            for (int i = 0; i < group.groupNode.Count; i++)
            {
                BmLerpBlendShapeNode aniNode = group.groupNode[i];
                aniNode.minInGroup = Mathf.Clamp01(group.start_time + group.space_time * i);
                aniNode.maxInGroup = Mathf.Clamp01(aniNode.minInGroup + group.node_len_time);                
            }
            Repaint();
            if (GUI.changed)
            {
                EditorUtility.SetDirty(target);
            }
        }

        #region Drag Frame
        Vector2 touchBegin;
        float startX;
        int _selectDir = 0;
        void MouseCheck(BmLerpBlendShapeNode node, Rect _rect, int _id, Rect _max, int _dir)
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
                        _selectDir = _dir;
                    }
                    break;
                case EventType.MouseUp:
                    selectId = -1;
                    _selectDir = 0;
                    break;
                case EventType.MouseDrag:
                    if (!_max.Contains(aEvent.mousePosition) || _selectDir != _dir)
                    {
                        return;
                    }
                    if (_id == selectId)
                    {
                        var t = aEvent.mousePosition - touchBegin;
                        float a = (startX + t.x) - _max.xMin;
                        if (_dir == Dir_Left)
                        {

                            node.minInGroup = a / _max.width;
                            node.minInGroup = Mathf.Clamp(node.minInGroup, 0, 1);

                        }
                        else if (_dir == Dir_Right)
                        {
                            node.maxInGroup = (startX + t.x) / _max.width;
                            node.maxInGroup = Mathf.Clamp(node.maxInGroup, 0, 1);

                        }


                        Repaint();
                    }

                    break;
                default:
                    break;
            }
        }
        #endregion

        #region Drag To Add
        void GetDrag()
        {
            if(GUILayout.Button("���"))
            {
                var group = target as BmLerpSkinmeshRenderBlendShape;                                
                BmLerpBlendShapeNodeWindow.Open(group.AddNode(), blendShapeName);
            }

            GUILayout.Space(10);
            if (GUI.changed)
            {
                EditorUtility.SetDirty(target);
            }
        }
        #endregion;

        void Preview()
        {
            isPreview = true;
        }

        void UnPreview()
        {
            isPreview = false;
        }

    

        private void OnEnable()
        {
            var group = target as BmLerpSkinmeshRenderBlendShape;
            group.CheckNode();
            InitBlendShapeInfo();
            EditorUtility.SetDirty(group);
        }


       
    }
}

