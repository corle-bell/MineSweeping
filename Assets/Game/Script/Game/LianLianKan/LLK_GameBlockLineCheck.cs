//*************************************************
//----Author:       Cyy 
//
//----CreateDate:   2022-07-28 11:23:03
//
//----Desc:         Create By BM
//
//**************************************************

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BmFramework.Core;

public class LLK_GameBlockLine
{
    public LLK_Block[] blocks;    
    public int dir; //0 , 1, 2, 3 //вСиосроб
    public int len;
    public LLK_GameBlockLine Init(int _dir, int _num)
    {
        blocks = new LLK_Block[_num];
        dir = _dir;
        return this;
    }
}

public class LLK_BlockPathNode
{
    public List<Vector2Int> pos = new List<Vector2Int>();
    public float distance;

    public void CalcDistance()
    {
        distance = 0;
        for(int i=1; i<pos.Count; i++)
        {
            distance += Vector2Int.Distance(pos[i - 1], pos[i]);
        }
    }
}


public class LLK_GameBlockLineCheck
{
    public LLK_Game list;
    public LLK_GameBlockLine[] lines;

    public readonly int[] dir = { -1, 0, 0, -1, 1, 0, 0, 1 };

    public Vector2Int begin;
    public List<LLK_BlockPathNode> PathList = new List<LLK_BlockPathNode>();
    public void Init(LLK_Game _list)
    {
        list = _list;
        int len = Mathf.Max(_list.col, _list.row)-1;
        lines = new LLK_GameBlockLine[4];
        for(int i=0; i< lines.Length; i++)
        {
            lines[i] = new LLK_GameBlockLine().Init(i, len);
        }
    }

    public void CalcLine(LLK_Block b0)
    {
        begin = new Vector2Int(b0.x, b0.y);
        lines.ForEach((LLK_GameBlockLine line, int i) =>
        {
            FillLine(line, b0.x, b0.y, i);
        });
    }

    private void FillLine(LLK_GameBlockLine _data, int x, int y, int _dir)
    {
        int len = _data.blocks.Length;
        int _dirBase = _dir * 2;
        _data.blocks.Clean();
        _data.len = 0;

        for (int i=1; i<=len; i++)
        {
            var block = list[x + dir[_dirBase] * i, y + dir[_dirBase + 1] * i];
            if(block != null && block.status==LLK_BlockStatus.None)
            {
                _data.blocks[i-1] = block;
                _data.len++;
            }
            else
            {
                break;
            }
        }
    }
    

    public void isLineIntersect(LLK_GameBlockLine line0, LLK_GameBlockLine line1, Vector2Int _endPos)
    {
        for (int i=0; i<line0.len; i++)
        {
            for (int q = 0; q < line1.len; q++)
            {
                if(list.isOneLinePass(line0.blocks[i], line1.blocks[q], true))
                {
                    var t = new LLK_BlockPathNode();
                    PathList.Add(t);

                    t.pos.Add(new Vector2Int(begin.x, begin.y));                    
                    t.pos.Add(new Vector2Int(line0.blocks[i].x, line0.blocks[i].y));
                    if (line0.blocks[i].id != line1.blocks[q].id)
                    {
                        t.pos.Add(new Vector2Int(line1.blocks[q].x, line1.blocks[q].y));
                    }
                    t.pos.Add(_endPos);

                    t.CalcDistance();
                }
            }
        }
    }

    public bool GetPath(ref List<LLK_Block> path, LLK_GameBlockLineCheck other)
    {
        path.Clear();
        PathList.Clear();
        for (int i=0; i<lines.Length; i++)
        {
            var myLine = lines[i];
            if (myLine.len == 0) continue;
            for (int q = 0; q < other.lines.Length; q++)
            {
                var otherLine = other.lines[q];
                if (otherLine.len == 0) continue;
                isLineIntersect(myLine, otherLine, other.begin);
            }
        }

        if(PathList.Count>0)
        {
            if(PathList.Count>1)
            {
                PathList.Sort((LLK_BlockPathNode l, LLK_BlockPathNode r) =>
                {
                    if (l.pos.Count < r.pos.Count 
                    || l.pos.Count == r.pos.Count && l.distance < r.distance)
                        return -1;
                    return 1;
                });
            }

            foreach (var i in PathList[0].pos)
            {
                path.Add(list[i.x, i.y]);
            }
        }

        return PathList.Count>0;
    }
}
