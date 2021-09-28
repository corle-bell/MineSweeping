using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEditor;

namespace Bm
{
	[CustomEditor(typeof(AnimationFrameEventController))]
	public class AnimationFrameEventControllerEditor : Editor
	{
		private string[] stateNames;
		private int selectIndex;
        public override void OnInspectorGUI()
        {
            EditorGUILayout.BeginHorizontal();
            if(stateNames!=null) selectIndex = EditorGUILayout.Popup("动画列表", selectIndex, stateNames);
			if (GUILayout.Button("添加"))
            {
                var controller = target as AnimationFrameEventController;
                int id = 0;
                if(controller.events==null)
                {
                    controller.events = new AnimationFrameFun[1];
                    id = 0;
                }
                else
                {
                    id = controller.events.Length;
                    System.Array.Resize(ref controller.events, controller.events.Length + 1);
                   
                }
                controller.events[id] = new AnimationFrameFun();
                controller.events[id].name = stateNames[selectIndex];
                controller.events[id].progress = 1.0f;
            }
            EditorGUILayout.EndHorizontal();

            base.OnInspectorGUI();

        }

        private void OnEnable()
        {
            var controller = target as AnimationFrameEventController;
            var runAnimator = controller.animator.runtimeAnimatorController;
            stateNames = new string[runAnimator.animationClips.Length];
            for (int i=0; i< stateNames.Length; i++)
            {
                stateNames[i] = runAnimator.animationClips[i].name;
            }
        }
    }
}
