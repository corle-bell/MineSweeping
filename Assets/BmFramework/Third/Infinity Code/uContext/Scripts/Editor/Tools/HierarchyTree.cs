/*           INFINITY CODE          */
/*     https://infinity-code.com    */

using UnityEditor;
using UnityEngine;

namespace InfinityCode.uContext.Tools
{
    [InitializeOnLoad]
    public static class HierarchyTree
    {
        private static Texture2D _endIcon;
        private static Texture2D _lineIcon;
        private static Texture2D _middleIcon;

        public static Texture2D endIcon
        {
            get
            {
                if (_endIcon == null) _endIcon = Resources.LoadIcon("Hierarchy_Tree_End");

                return _endIcon;
            }
        }

        public static Texture2D lineIcon
        {
            get
            {
                if (_lineIcon == null) _lineIcon = Resources.LoadIcon("Hierarchy_Tree_Line");
                return _lineIcon;
            }
        }

        public static Texture2D middleIcon
        {
            get
            {
                if (_middleIcon == null) _middleIcon = Resources.LoadIcon("Hierarchy_Tree_Middle");
                return _middleIcon;
            }
        }

        static HierarchyTree()
        {
            HierarchyItemDrawer.Register("HierarchyTree", DrawTree);
        }

        private static void DrawTree(HierarchyItem item)
        {
            if (!Prefs.hierarchyTree || Event.current.type != EventType.Repaint) return;
            if (item == null || item.gameObject == null) return;

            Transform transform = item.gameObject.transform;
            Transform parent = transform.parent;
            if (parent == null) return;

            Rect rect = item.rect;

            rect.width = 36;
            rect.x -= 32;

            Color color = GUI.color;
            GUI.color = Color.gray;

            if (parent.childCount == 1 || transform.GetSiblingIndex() == parent.childCount - 1)
            {
                GUI.DrawTexture(rect, endIcon, ScaleMode.ScaleToFit);
            }
            else
            {
                GUI.DrawTexture(rect, middleIcon, ScaleMode.ScaleToFit);
            }

            while (parent != null && parent.parent != null)
            {
                rect.x -= 14;

                if (parent.GetSiblingIndex() < parent.parent.childCount - 1)
                {
                    GUI.DrawTexture(rect, lineIcon, ScaleMode.ScaleToFit);
                }

                parent = parent.parent;
            }

            GUI.color = color;
        }
    }
}