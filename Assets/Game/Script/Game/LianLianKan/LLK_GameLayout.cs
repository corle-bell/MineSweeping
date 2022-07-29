//*************************************************
//----Author:       Cyy 
//
//----CreateDate:   2022-07-27 17:48:46
//
//----Desc:         Create By BM
//
//**************************************************

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BmFramework.Core;
using DG.Tweening;

public partial class LLK_Game : MonoBehaviour, IResLoadCallback
{
    public int row;
    public int col;

    public AnimationCurve bounceCurve;

    private int max_row;
    private int max_col;
    
    private int loadNum;

    LLK_Block[] blockList;

    public LLK_Block this[int i0 , int i1]
    {
        get
        {
            int _index = i1 * max_row + i0;

            return _index>=0 && _index<blockList.Length?blockList[i1 * max_row + i0]:null;
        }
        set
        {
            blockList[i1 * max_row + i0] = value;
        }
    }

    public void InitLayout()
    {

        max_row = row + 2;
        max_col = col + 2;
        int len = max_row * max_col;

        blockList = new LLK_Block[len];


        RectTransform rectTransform = transform as RectTransform;
        rectTransform.sizeDelta = new Vector2(max_row * 70 + (max_row - 1) * 5 + 20, max_col * 70 + (max_col - 1) * 5 + 20);
        rectTransform.position = new Vector3(Screen.width / 2, Screen.height / 2, 0);

        ResManager.instance.LoadRes("LianLianKan/Block", len, this);

        loadNum = 0;
    }

    public void FillLayout()
    {
        //获得要填充的格子
        int[] idArr = new int[row * col];
        int id = 0;
        for(int i=0; i<row; i++)
        {
            for (int c = 0; c < col; c++)
            {
                idArr[id++] = (c+1) * max_row + (i+1);
            }
        }

        

        //填充
        int EndIndex = idArr.Length;
        int PairCount = idArr.Length/2;
        int PairId = 0;
        for (int i = 0; i < PairCount; i++)
        {
            PairId = Random.Range(0, 6);
            Color color = Color.HSVToRGB(((float)PairId / 6), 1, 1);

            for (int c = 0; c<2; c++)
            {
                int lastIndex = EndIndex - 1;
                int index = Random.Range(0, EndIndex);

                var block = blockList[idArr[index]];

                block.type = LLK_BlockType.Block;                
                block.pairId = PairId;
                block.SetColor(color);
                block.SetStatus(LLK_BlockStatus.Live);

                idArr.Swap(index, lastIndex);

                EndIndex--;
            }
        }
    }

    public void OnLoad(string path, Object _obj, object _data)
    {
        var gameObj = _obj as GameObject;

        gameObj.transform.parent = transform;

        blockList[loadNum] = gameObj.GetComponent<LLK_Block>();
        blockList[loadNum].Init(loadNum, loadNum % max_row, loadNum / max_row, this);

        gameObj.transform.localScale = Vector3.zero;
        gameObj.transform.DOScale(1f, 0.45f).SetEase(bounceCurve);

        loadNum++;
        if(loadNum>=blockList.Length)
        {
            FillLayout();
            BeginGame();
        }
    }
}
