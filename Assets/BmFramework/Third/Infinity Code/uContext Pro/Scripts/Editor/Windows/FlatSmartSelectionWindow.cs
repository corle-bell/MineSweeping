﻿/*           INFINITY CODE          */
/*     https://infinity-code.com    */

using System.Collections.Generic;
using System.Linq;
using InfinityCode.uContext;
using InfinityCode.uContext.Tools;
using InfinityCode.uContext.UnityTypes;
using InfinityCode.uContext.Windows;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace InfinityCode.uContextPro.Tools
{
    public class FlatSmartSelectionWindow : AutoSizePopupWindow
    {
        private static string filterText;
        private static bool resetSelection;
        private static List<FlatItem> activeItems;
        private static List<FlatItem> flatItems;
        private static FlatSmartSelectionWindow _instance;

        public static FlatSmartSelectionWindow instance
        {
            get { return _instance; }
        }

        private static TreeViewState treeViewState;
        private static FlatTreeView treeView;

        private static bool DrawFilterTextField()
        {
            GUI.SetNextControlName("uContextSmartSelectionSearchTextField");
            EditorGUI.BeginChangeCheck();
            filterText = EditorGUILayoutEx.ToolbarSearchField(filterText);
            bool changed = EditorGUI.EndChangeCheck();

            if (resetSelection && Event.current.type == EventType.Repaint)
            {
                GUI.FocusControl("uContextSmartSelectionSearchTextField");
                resetSelection = false;
            }

            return changed;
        }

        protected override void OnContentGUI()
        {
            Event e = Event.current;
            EventType type = e.type;

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Select GameObject");
            if (GUILayoutUtils.ToolbarButton("?")) Links.OpenDocumentation("smart-selection");
            EditorGUILayout.EndHorizontal();

            bool filterChanged = DrawFilterTextField();

            bool needReload = false;

            if (filterChanged || activeItems == null)
            {
                if (string.IsNullOrEmpty(filterText)) activeItems = flatItems;
                else
                {
                    string pattern = SearchableItem.GetPattern(filterText);
                    activeItems = flatItems.Where(p => p.UpdateAccuracy(pattern) > 0).OrderByDescending(p => p.accuracy).ToList();
                }

                needReload = true;
            }

            if (treeView == null)
            {
                treeViewState = new TreeViewState();
                treeView = new FlatTreeView(treeViewState);
                needReload = false;
            }

            if (needReload) treeView.Reload();

            Rect lastRect = GUILayoutUtility.GetLastRect();
            Rect rect = GUILayoutUtility.GetRect(0, 100000, 0, Mathf.Min(Prefs.defaultWindowSize.y - lastRect.y, treeView.totalHeight));
            treeView.OnGUI(rect);

            if (type == EventType.KeyDown)
            {
                if (e.keyCode == KeyCode.Escape) Close();
                else if (e.keyCode == KeyCode.UpArrow || e.keyCode == KeyCode.DownArrow)
                {
                    if (GUI.GetNameOfFocusedControl() == "uContextSmartSelectionSearchTextField")
                    {
                        treeView.SetFocusAndEnsureSelectedItem();
                        e.Use();
                    }
                }
                else if (e.keyCode == KeyCode.Return || e.keyCode == KeyCode.KeypadEnter)
                {
                    IList<int> selection = treeView.GetSelection();
                    int id = selection.Count > 0 ? selection.First() : 0;
                    SelectByDisplayID(id);
                }
            }
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            foreach (FlatItem item in flatItems) item.Dispose();
            flatItems.Clear();

            Waila.mode = 0;
            UnityEditor.Tools.hidden = false;
            filterText = null;
            activeItems = null;
            scrollPosition = Vector2.zero;
            treeView = null;
            treeViewState = null;

            if (SceneView.lastActiveSceneView != null) SceneView.lastActiveSceneView.Focus();
        }

        private void OnEnable()
        {
            resetSelection = true;
            uContext.Tools.Highlighter.Highlight(null);
        }

        private void SelectByDisplayID(int id)
        {
            if (id < treeView.items.Count)
            {
                GameObject target = treeView.items[id];
                if (Event.current.control || Event.current.shift) SelectionRef.Add(target);
                else Selection.activeGameObject = target;
            }

            Close();
        }

        public new static FlatSmartSelectionWindow Show()
        {
            if (flatItems == null) flatItems = new List<FlatItem>();
            else flatItems.Clear();

            flatItems.AddRange(Waila.targets.Select(t => new FlatItem(t)));
            if (flatItems.Count == 0) return null;

            Vector2 size = Prefs.defaultWindowSize;
            Vector2 position = Event.current.mousePosition - new Vector2(size.x / 2, -30);

            position = GUIUtility.GUIToScreenPoint(position);

            Rect rect = new Rect(position, size);

            _instance = CreateInstance<FlatSmartSelectionWindow>();
            _instance.minSize = Vector2.zero;
            _instance.position = rect;
            _instance.adjustHeight = AutoSize.top;
            _instance.ShowPopup();
            _instance.Focus();
            return _instance;
        }

        internal class FlatTreeView : TreeView
        {
            public List<GameObject> items;

            public FlatTreeView(TreeViewState state) : base(state)
            {
                Reload();
            }

            public FlatTreeView(TreeViewState state, MultiColumnHeader multiColumnHeader) : base(state, multiColumnHeader)
            {
                Reload();
            }

            protected override TreeViewItem BuildRoot()
            {
                var root = new TreeViewItem { id = -1, depth = -1, displayName = "Root" };
                var allItems = new List<TreeViewItem>();

                int nextID = 0;
                items = new List<GameObject>();

                for (int i = 0; i < activeItems.Count; i++)
                {
                    FlatItem flatItem = activeItems[i];
                    TreeViewItem item = new TreeViewItem {id = nextID++, depth = 0, displayName = flatItem.name};
                    allItems.Add(item);
                    items.Add(flatItem.target);

                    Transform parent = flatItem.target.transform.parent;
                    while (parent != null)
                    {
                        GameObject go = parent.gameObject;
                        item = new TreeViewItem { id = nextID++, depth = 1, displayName = go.name };
                        items.Add(go);
                        allItems.Add(item);
                        parent = parent.parent;
                    }
                }

                SetupParentsAndChildrenFromDepths(root, allItems);

                return root;
            }

            protected override void SingleClickedItem(int id)
            {
                instance.SelectByDisplayID(id);
            }
        }

        internal class FlatItem: SearchableItem
        {
            public string name;
            public GameObject target;

            public FlatItem(GameObject target)
            {
                this.target = target;
                name = target.name;
            }

            public void Dispose()
            {
                target = null;
            }

            protected override int GetSearchCount()
            {
                return 1;
            }

            protected override string GetSearchString(int index)
            {
                return name;
            }
        }
    }
}