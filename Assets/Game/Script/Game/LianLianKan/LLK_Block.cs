//*************************************************
//----Author:       Cyy 
//
//----CreateDate:   2022-07-27 17:25:13
//
//----Desc:         Create By BM
//
//**************************************************

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BmFramework.Core;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public enum LLK_BlockType
{
    None=0,
    Block,
}
public enum LLK_BlockStatus
{
    None,
    Live,
}

public interface ILLK_Block_Select
{
    void OnSelect(LLK_Block block);
}

public class LLK_Block : MonoBehaviour, IPointerClickHandler
{
    public LLK_BlockType type;
    public LLK_BlockStatus status;
    public Image graphic;
    public int pairId;
    public int id;
    public ILLK_Block_Select selectReciver;

    public int liveFlag
    {
        get
        {
            return status == LLK_BlockStatus.None ? 1 : 0;
        }
    }
    public bool isSelect
    {
        get
        {
            return _isSelect;
        }
        set
        {
            _isSelect = value;
            graphic.SetAlpha(_isSelect ? 0.5f:1);
        }
    }

    public int x {
        set
        {
            grid_pos[0] = value;
        }
        get
        {
            return grid_pos[0];
        }
    }

    public int y
    {
        set
        {
            grid_pos[1] = value;
        }
        get
        {
            return grid_pos[1];
        }
    }
    public int this[int index]
    {
        set
        {
            grid_pos[index] = value;
        }
        get
        {
            return grid_pos[index];
        }
    }

    [HideInInspector]
    public int[] grid_pos = new int[] { 0, 0 };

    protected bool _isSelect = false;

    public void Init(int _id, int _x, int _y, ILLK_Block_Select _reciver)
    {
        this.id = _id;
        this.x = _x;
        this.y = _y;
        selectReciver = _reciver;
        SetStatus(LLK_BlockStatus.None);
    }

    public void SetColor(Color _c)
    {
        switch(type)
        {
            case LLK_BlockType.Block:
                graphic.color = _c;
                break;
        }
    }

    
    public void SetStatus(LLK_BlockStatus _s, float _duration=0)
    {
        status = _s;
        if(status==LLK_BlockStatus.None)
        {
            if (_duration == 0)
                graphic.SetAlpha(0);
            else
                graphic.FadeTo(0, 0.35f);
        }
        else
        {
            graphic.SetAlpha(1);
        }
    }

    private void Reset()
    {
        graphic = GetComponent<Image>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (status == LLK_BlockStatus.None) return;
        
        isSelect = !isSelect;
        selectReciver.OnSelect(this);

    }
}
