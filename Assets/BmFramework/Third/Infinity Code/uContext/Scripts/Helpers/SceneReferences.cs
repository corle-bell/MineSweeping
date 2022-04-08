/*           INFINITY CODE          */
/*     https://infinity-code.com    */

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace InfinityCode.uContext
{
    public class SceneReferences : MonoBehaviour
    {
        public static Action<SceneReferences> OnCreate;
        public static Action OnUpdateInstances;

        public static List<SceneReferences> instances;

        public List<SceneBookmark> bookmarks = new List<SceneBookmark>();
        public List<HierarchyBackground> hierarchyBackgrounds = new List<HierarchyBackground>();

        public static SceneReferences Get(Scene scene, bool createIfMissed = true)
        {
            SceneReferences r = instances.FirstOrDefault(i => i != null && i.gameObject.scene == scene);
            if (r != null) return r;
            if (!createIfMissed) return null;

            Scene activeScene = SceneManager.GetActiveScene();
            bool sceneChanged = false;
            if (activeScene != scene)
            {
                SceneManager.SetActiveScene(scene);
                sceneChanged = true;
            }
            GameObject go = new GameObject("uContext References");
            go.tag = "EditorOnly";
            r = go.AddComponent<SceneReferences>();
            if (OnCreate != null) OnCreate(r);
            instances.Add(r);
            if (sceneChanged) SceneManager.SetActiveScene(activeScene);

            return r;
        }

        public HierarchyBackground GetBackground(GameObject target)
        {
            foreach (HierarchyBackground b in hierarchyBackgrounds)
            {
                if (b.gameObject == target) return b;
            }

            Transform t = target.transform.parent;
            if (t == null) return null;

            foreach (HierarchyBackground b in hierarchyBackgrounds)
            {
                if (!b.recursive) continue;
                t = target.transform.parent;

                do
                {
                    if (b.gameObject == t.gameObject) return b;
                    t = t.parent;
                } while (t != null);
            }

            return null;
        }

        public static void UpdateInstances()
        {
            instances = FindObjectsOfType<SceneReferences>().ToList();
            if (OnUpdateInstances != null) OnUpdateInstances();
        }

        [Serializable]
        public class HierarchyBackground
        {
            public GameObject gameObject;
            public Color color;
            public bool recursive = false;
        }
    }
}