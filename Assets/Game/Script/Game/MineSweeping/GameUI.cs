using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BmFramework.Core;

public partial class Game : MonoBehaviour
{
    public bool isFlagMode;
    public Text status;
    
    public void OnClick()
    {
        UIManager.instance.OpenUI<UIChangeScene> ("Reload");
    }

    public void OnFlag(Toggle toggle)
    {
        audioData.Click();

        isFlagMode = toggle.isOn;
        status.text = string.Format("当前操作为:{0}", isFlagMode? "标记":"点击");
    }

}
