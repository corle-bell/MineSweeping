using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawManager : MonoBehaviour
{
    public DrawPhycisLine[] drawPhycisLines;
    public int id;
    public DrawPhycisLine current;

    [EnumName("分布密度")]
    public float ColliderLen = 0.12f;

    [EnumName("单个碰撞体大小")]
    public float ColliderRadius = 0.1f;

    [EnumName("单个碰撞体Z轴长度")]
    public float ColliderZLen = 0.2f;

    [EnumName("距离修正最大数量")]
    public int ColliderFixNum = 20;

    private void Awake()
    {
        drawPhycisLines = GetComponentsInChildren<DrawPhycisLine>();
    }
    // Start is called before the first frame update
    void Start()
    {
        id = 0;
        SetCurrentParams(id);
    }

    private void SetCurrentParams(int _id)
    {
        current = drawPhycisLines[_id];

        current.ColliderLen = this.ColliderLen;
        current.ColliderRadius = this.ColliderRadius;
        current.ColliderZLen = this.ColliderZLen;
        current.ColliderFixNum = this.ColliderFixNum;
    }

    // Update is called once per frame
    void Update()
    {
        current.Logic();
        if (Input.GetMouseButtonUp(0))
        {
            NextDraw();
        }
    }

    public void ClearAllLines()
    {
        foreach (var item in drawPhycisLines)
        {
            item.Clear();
        }
        id = 0;
        current = drawPhycisLines[id];
    }

    public void ClearLastLines()
    {
        LastDraw();
        current.Clear();
    }


    void NextDraw()
    {
        id++;
        id = id >= drawPhycisLines.Length ? 0 : id;
        SetCurrentParams(id);
    }

    void LastDraw()
    {
        id--;
        id = id <0 ? drawPhycisLines.Length-1 : id;
        SetCurrentParams(id);
    }
}
