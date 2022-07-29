//*************************************************
//----Author:       Cyy 
//
//----CreateDate:   2022-07-29 15:57:14
//
//----Desc:         Create By BM
//
//**************************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BmFramework.Core;

public class ClickBackToMenu : MonoBehaviour
{
    public string sceneName = "Menu";
    public void BackToScene()
    {
        UIManager.instance.OpenUI<UIChangeScene>(sceneName);
    }
}
