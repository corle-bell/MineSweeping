//*************************************************
//----Author:       Cyy 
//
//----CreateDate:   2022-07-27 17:41:17
//
//----Desc:         Create By BM
//
//**************************************************

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BmFramework.Core;
using UnityEngine.EventSystems;
using UnityEngine.UI.Extensions;

public partial class LLK_Game : MonoBehaviour, ILLK_Block_Select
{
    public LLK_Block[] selectBlocks = new LLK_Block[2];
    public int selectIndex = 0;

    public LLK_GameBlockLineCheck checkBegin;
    public LLK_GameBlockLineCheck checkEnd;

    public UILineRendererList pathDrawer;
    public List<LLK_Block> path = new List<LLK_Block>();
    // Start is called before the first frame update
    void Start()
    {
        //code here
        InitLayout();

        checkBegin = new LLK_GameBlockLineCheck();
        checkBegin.Init(this);

        checkEnd = new LLK_GameBlockLineCheck();
        checkEnd.Init(this);
    }

    public void BeginGame()
    {
        pathDrawer.ClearPoints();
    }

    public void OnSelect(LLK_Block block)
    {
        selectBlocks[selectIndex] = block;
        selectIndex++;

        if(selectIndex >= selectBlocks.Length)
        {
            CheckBlock();
        }
    }

    public void CheckBlock()
    {
        if(selectBlocks[0].pairId!=selectBlocks[1].pairId || selectBlocks[0].id == selectBlocks[1].id)
        {
            ClearSelectBlocks();
            return;
        }

        bool isRight = false;
        //同一方向且连同
        if (isOneLinePass(selectBlocks[0], selectBlocks[1]))
        {
            isRight = true;

            path.Clear();
            path.Add(selectBlocks[0]);
            path.Add(selectBlocks[1]);
        }
        else
        {
            checkBegin.CalcLine(selectBlocks[0]);
            checkEnd.CalcLine(selectBlocks[1]);

            isRight = checkBegin.GetPath(ref path, checkEnd);
        }

        if(isRight)
        {
            pathDrawer.SetAlpha(1f);
            pathDrawer.ClearPoints();
            foreach (var b in path)
            {
                var pos = pathDrawer.transform.InverseTransformPoint(b.transform.position);                
                pathDrawer.AddPoint(pos);
            }

            pathDrawer.FadeTo(0f, 0.45f);
        }



        ClearSelectBlocks(isRight);
    }

    public bool isOneLinePass(LLK_Block b0, LLK_Block b1, bool isIncludeInput = false)
    {
        return isLinePass(b0, b1, 0, isIncludeInput) || isLinePass(b0, b1, 1, isIncludeInput);
    }


    /// <summary>
    /// 是否通畅
    /// </summary>
    /// <param name="b0">开始节点</param>
    /// <param name="b1">结束</param>
    /// <param name="_dir">方向 横或竖 0 1</param>
    /// <param name="isIncludeInput">检测长度是否包含边界的格子</param>
    /// <returns></returns>
    private bool isLinePass(LLK_Block b0, LLK_Block b1, int _dir, bool isIncludeInput=false)
    {
        int inverse_dir = Mathf.Abs(_dir - 1);
        if (b0[inverse_dir] == b1[inverse_dir] && b0[_dir] == b1[_dir])
        {
            return true;
        }
        else if (b0[inverse_dir] == b1[inverse_dir])
        {
            int begin = Mathf.Min(b1[_dir], b0[_dir]) + 1;
            int len = Mathf.Abs(b1[_dir] - b0[_dir]) - 1;
            int target = len + (isIncludeInput ? 1 : 0);
            int num = 0;
            for (int i = 0; i <= len; i++)
            {
                int r = begin + i;
                num += _dir==0?this[r, b0[inverse_dir]].liveFlag:this[b0[inverse_dir], r].liveFlag;
            }
            return num == target;
        }
        return false;
    }

    private void ClearSelectBlocks(bool isChangeStatus = false)
    {
        selectBlocks[0].isSelect = selectBlocks[1].isSelect = false;

        if (isChangeStatus)
        {
            selectBlocks[0].SetStatus(LLK_BlockStatus.None);
            selectBlocks[1].SetStatus(LLK_BlockStatus.None);
        }

        selectIndex = 0;
        selectBlocks.Clean();
    }
}
