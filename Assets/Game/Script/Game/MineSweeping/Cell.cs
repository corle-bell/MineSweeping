using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Cell : MonoBehaviour, IPointerDownHandler
{
    public Text NumberText;
    public Image LandmineImg;
    public Image FlagImg;
    public Image CoverImg;
    public int id;
    public Game game;


    public int Number {
        get
        {
            return num;
        }
        set
        {
            num = value;
            NumberText.text = num.ToString();
            NumberText.SetAlpha(num > 0 ? 1 : 0);
        }
    }

    public bool isLandmine
    {
        get
        {
            return _isLandmine;
        }
        set
        {
            _isLandmine = value;
            LandmineImg.SetAlpha(_isLandmine ? 1 : 0);            
        }
    }

    public bool isFlag
    {
        get
        {
            return _isFlag;
        }
        set
        {
            _isFlag = value;
            FlagImg.SetAlpha(_isFlag ? 1 : 0);
            if(_isLandmine)
            {
                LandmineImg.SetAlpha(_isFlag ? 0.3f : 0);
            }
        }
    }

    public bool isShow
    {
        get
        {
            return _isShow;
        }
        set
        {
            _isShow = value;
            CoverImg.SetAlpha(_isShow ? 0 : 1);
        }
    }

    private bool _isShow;
    private bool _isFlag;
    private bool _isLandmine;
    private int num = 0;

    public void Init(Game _game, int _id)
    {
        id = _id;
        game = _game;

        ResetCell();
    }

    /// <summary>
    /// 初始化格子数据
    /// </summary>
    public void ResetCell()
    {
        isLandmine = false;
        Number = 0;
        isFlag = false;
        isShow = false;
    }

    private void Flag()
    {
        if (!_isShow)
        {
            isFlag = !isFlag;

            game.audioData.Flag();
        }
    }

    private void Click()
    {
        if (CanShow())
        {
            game.CheckCell(id);
        }
    }

    /// <summary>
    /// 格子点击事件
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerDown(PointerEventData eventData)
    {

#if UNITY_STANDALONE || UNITY_EDITOR
        switch (eventData.pointerId)
        {
            case -1://left button
                if (game.isFlagMode)
                {
                    Flag();
                }
                else
                {
                    Click();
                }
                break;
            case -2://right button
                Flag();
                break;
        }
#else
        if(game.isFlagMode)
        {
            Flag();
        }
        else
        {
            Click();
        }
#endif
    }

    /// <summary>
    /// 当前格子是否可以被显示
    /// </summary>
    /// <returns></returns>
    public bool CanShow()
    {
        return !isFlag && !_isShow;
    }

    /// <summary>
    /// 显示格子
    /// </summary>
    /// <returns></returns>
    public bool Show()
    {
        if (_isShow) return false;

        if(!_isFlag && !_isLandmine)
        {
            isShow = true;
            game.ShowNumber++;
        }
        return num > 0 || _isFlag || _isLandmine;
    }
}
