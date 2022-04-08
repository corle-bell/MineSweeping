/*************************************************
----Author:       Cyy 

----CreateDate:   2022-04-08 15:58:02

----Desc:         Create By BM
**************************************************/ 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BmFramework.Core;

public class GameController_DrawLine : MonoBehaviour
{
    public DrawManager drawManager;
    public GameObject ballContainer;
    public GameObject gameWin;
    private Rigidbody[] balls;
    int ballNumber;
    // Start is called before the first frame update
    void Start()
    {
        balls = ballContainer.GetComponentsInChildren<Rigidbody>();
    }

    public void OnStart()
    {
        foreach(var i in balls)
        {
            i.isKinematic = false;
        }
    }

    public void OnClear()
    {
        drawManager.ClearAllLines();
    }

    public void OnReload()
    {
        SceneHelper.Reload();
    }

    
    public void OnEnding()
    {
        ballNumber++;
        if(ballNumber>=balls.Length)
        {
            gameWin.SetActive(true);
        }        
    }
}
