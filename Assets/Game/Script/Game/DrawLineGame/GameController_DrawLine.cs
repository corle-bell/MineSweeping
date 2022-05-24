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
    public GameObject drawManager;
    public GameObject ballContainer;
    public GameObject gameWin;

    private Rigidbody[] balls;
    private Rigidbody2D[] balls2D;
    int ballNumber;
    int ballCount;
    // Start is called before the first frame update
    void Start()
    {
        balls = ballContainer.GetComponentsInChildren<Rigidbody>();
        balls2D = ballContainer.GetComponentsInChildren<Rigidbody2D>();
    }

    public void OnStart()
    {
        foreach(var i in balls)
        {
            i.isKinematic = false;
        }

        foreach (var i in balls2D)
        {
            i.isKinematic = false;
        }

        ballCount = balls.Length + balls2D.Length;
    }

    public void OnClear()
    {
        drawManager.SendMessage("ClearAllLines");
    }

    public void OnReload()
    {
        SceneHelper.Reload();
    }

    
    public void OnEnding()
    {
        ballNumber++;
        if(ballNumber>= ballCount)
        {
            gameWin.SetActive(true);
        }        
    }
}
