/*           INFINITY CODE          */
/*     https://infinity-code.com    */

using System;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace InfinityCode.uContext
{
    [InitializeOnLoad]
    public static class SceneManagerHelper
    {
        static SceneManagerHelper()
        {
            EditorApplication.delayCall += () =>
            {
                EditorSceneManager.sceneOpened -= OnSceneOpened;
                EditorSceneManager.sceneOpened += OnSceneOpened;
                
                EditorSceneManager.sceneClosed -= OnSceneClosed;
                EditorSceneManager.sceneClosed += OnSceneClosed;
            };

            SceneReferences.OnCreate += OnCreateSceneReferences;
            UpdateInstances();
        }

        private static void OnCreateSceneReferences(SceneReferences r)
        {
            r.gameObject.hideFlags = Prefs.hideSceneReferences ? HideFlags.HideInHierarchy : HideFlags.None;
            Undo.RegisterCreatedObjectUndo(r.gameObject, "References");
        }

        private static void OnSceneClosed(Scene scene)
        {
            EditorApplication.delayCall += UpdateInstances;
        }

        private static void OnSceneOpened(Scene scene, OpenSceneMode mode)
        {
            EditorApplication.delayCall += UpdateInstances;
        }

        public static void UpdateInstances()
        {
            HideFlags hideFlags = Prefs.hideSceneReferences? HideFlags.HideInHierarchy: HideFlags.None;
            
            SceneReferences.UpdateInstances();
            foreach (SceneReferences r in SceneReferences.instances)
            {
                GameObject go = r.gameObject;
                if (go.hideFlags == hideFlags) continue;

                go.hideFlags = hideFlags;
                EditorUtility.SetDirty(go);
            }
        }
    }
}