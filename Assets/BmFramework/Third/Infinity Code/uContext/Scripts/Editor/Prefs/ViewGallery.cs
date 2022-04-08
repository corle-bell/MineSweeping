﻿/*           INFINITY CODE          */
/*     https://infinity-code.com    */

using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace InfinityCode.uContext
{
    public static partial class Prefs
    {
        public static bool viewGalleryHotKey = true;
        public static bool createViewStateFromSelection = true;
        public static bool restoreViewStateFromSelection = true;
        public static bool showViewStateInScene = true;

        public static KeyCode viewGalleryKeyCode = KeyCode.V;
        public static KeyCode createViewStateFromSelectionKeyCode = KeyCode.Slash;
        public static KeyCode restoreViewStateFromSelectionKeyCode = KeyCode.Slash;

        public static EventModifiers viewGalleryModifiers = EventModifiers.Alt | EventModifiers.Shift;

#if !UNITY_EDITOR_OSX
        public static EventModifiers createViewStateFromSelectionModifiers = EventModifiers.Control;
        public static EventModifiers restoreViewStateFromSelectionModifiers = EventModifiers.Control | EventModifiers.Shift;
#else
        public static EventModifiers createViewStateFromSelectionModifiers = EventModifiers.Command;
        public static EventModifiers restoreViewStateFromSelectionModifiers = EventModifiers.Command | EventModifiers.Shift;
#endif

        public class ViewGalleryManager : StandalonePrefManager<ViewGalleryManager>, IHasShortcutPref
        {
#if !UCONTEXT_PRO
            private const string createLabel = "Create View State For Selection (PRO)";
            private const string restoreLabel = "Restore View State For Selection (PRO)";
            private const string showViewStateInSceneLabel = "Show ViewState In SceneView (PRO)";
#else
            private const string createLabel = "Create View State For Selection";
            private const string restoreLabel = "Restore View State For Selection";
            private const string showViewStateInSceneLabel = "Show ViewState In SceneView";
#endif
            public override IEnumerable<string> keywords
            {
                get
                {
                    return new[]
                    {
                        "Create View State For Selection",
                        "Restore View State For Selection",
                        "View Gallery",
                    };
                }
            }

            public override float order
            {
                get { return -40; }
            }

            public override void Draw()
            {
                DrawFieldWithHotKey("View Gallery", ref viewGalleryHotKey, ref viewGalleryKeyCode, ref viewGalleryModifiers, EditorStyles.label, 17);
                DrawFieldWithHotKey(createLabel, ref createViewStateFromSelection, ref createViewStateFromSelectionKeyCode, ref createViewStateFromSelectionModifiers, EditorStyles.label, 17);
                DrawFieldWithHotKey(restoreLabel, ref restoreViewStateFromSelection, ref restoreViewStateFromSelectionKeyCode, ref restoreViewStateFromSelectionModifiers, EditorStyles.label, 17);
                showViewStateInScene = EditorGUILayout.ToggleLeft("Show View State In SceneView (Hot Key - ALT)", showViewStateInScene);
            }

            public IEnumerable<Shortcut> GetShortcuts()
            {
                if (!viewGalleryHotKey) return new Shortcut[0];

                return new[]
                {
                    new Shortcut("Open View Gallery", "Everywhere", viewGalleryModifiers, viewGalleryKeyCode),
                    new Shortcut(createLabel, "Everywhere", createViewStateFromSelectionModifiers, createViewStateFromSelectionKeyCode),
                    new Shortcut(restoreLabel, "Everywhere", restoreViewStateFromSelectionModifiers, restoreViewStateFromSelectionKeyCode),
                    new Shortcut(showViewStateInSceneLabel, "Scene View", EventModifiers.Alt),
                };
            }
        }
    }
}