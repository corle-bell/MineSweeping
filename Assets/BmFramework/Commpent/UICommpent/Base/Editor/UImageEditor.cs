using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;


[CustomEditor(typeof(UIStatusImage))]
public class UIStatusImageEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Set Native Size", GUILayout.MinHeight(20)))
        {
            UIStatusImage tt = target as UIStatusImage;
            tt.rectTransform.sizeDelta = new Vector2(tt.sprite.rect.width, tt.sprite.rect.height);
        }
    }
}

[CustomEditor(typeof(UIImage_AjustSprite))]
public class UIImage_AjustSpriteEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Set Native Size", GUILayout.MinHeight(20)))
        {
            UIImage_AjustSprite tt = target as UIImage_AjustSprite;
            tt.rectTransform.sizeDelta = new Vector2(tt.sprite.rect.width, tt.sprite.rect.height);
        }
    }
}

