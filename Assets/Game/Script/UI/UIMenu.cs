//*************************************************
//----Author:       Cyy 
//
//----CreateDate:   2022-07-28 17:39:10
//
//----Desc:         Create By BM
//
//**************************************************

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BmFramework.Core;

public partial class UIMenu : UIAnimationRoot
{
    // Start is called before the first frame update
    void Start()
    {
        //code here
    }

    public void OnButtonClick(string sceneName)
    {
        this.Close();
        UIManager.instance.OpenUI<UIChangeScene>(sceneName);
    }
}
