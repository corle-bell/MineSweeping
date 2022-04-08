using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BmFramework.Core;
using DG.Tweening;
public partial class Game : MonoBehaviour
{

    public int Width = 10;
    public int Height = 10;
    public int LandmineNumber=10;

    public bool isWin;

    [HideInInspector]
    public Cell[] cells;

    [HideInInspector]
    public int ShowNumber = 0;

    public AudioData audioData;

    int AllNumber;
    
    int[] LandmineIndex;

    /// <summary>
    /// 方便遍历周围格子
    /// </summary>
    int[] Around_X_Gen = new int[8] { -1, 0, 1, -1, 1, -1, 0, 1 };
    int[] Around_Y_Gen = new int[8] { -1, -1, -1, 0, 0, 1, 1, 1 };


    // Start is called before the first frame update
    void Start()
    {
        UIManager.instance.OpenUI<UIGameSelect>(this);

        BmDebug.Info("游戏开始!");
    }


    public void GameInit()
    {
        AllNumber = Width * Height;

        RectTransform rectTransform = transform as RectTransform;
        rectTransform.sizeDelta = new Vector2(Width * 70 + (Width - 1) * 5 + 20, Height * 70 + (Height - 1) * 5 + 20);
        rectTransform.position = new Vector3(Screen.width / 2, Screen.height / 2 + 60, 0);

       

        NotifacitionManager.AddObserver(NotifyType.Msg_Resouce_Load, this.MsgEvent);

        ResManager.instance.frameLoadNum = Width;
        ResManager.instance.LoadRes("Cell", AllNumber);
    }

    void MsgEvent(NotifyEvent data)
    {
        if (data.Cmd == "All")
        {
            cells = GetComponentsInChildren<Cell>();
            for (int i = 0; i < cells.Length; i++)
            {
                var cell = cells[i];
                cell.Init(this, i);
            }

            NotifacitionManager.RemoveObserver(NotifyType.Msg_Resouce_Load, this.MsgEvent);

            GameStart();
            return;
        }

        GameObject tmp = data.GameObj as GameObject;
        tmp.transform.SetParent(transform, false);
    }



    /// <summary>
    /// 随机布雷
    /// </summary>
    void Genarate()
    {
        
        LandmineIndex = new int[LandmineNumber];

        //获取随机的地雷id
        int[] randomArr = new int[AllNumber];
        for(int i=0; i<AllNumber; i++)
        {
            randomArr[i] = i;
        }

        int EndIndex = AllNumber;
        for(int i=0; i<LandmineNumber; i++)
        {
            int lastIndex = EndIndex - 1;
            int id = Random.Range(0, EndIndex);
            
            LandmineIndex[i] = randomArr[id];

            int lastId = randomArr[lastIndex];
            randomArr[lastIndex] = randomArr[id];
            randomArr[id] = lastId;

            EndIndex--;
        }

        for (int i = 0; i < LandmineNumber; i++)
        {
            cells[LandmineIndex[i]].isLandmine = true;
        }

        //给地雷周围的格子标记数量
        for (int i = 0; i < LandmineNumber; i++)
        {
            FlagAround(LandmineIndex[i]);
        }
    }

    
    /// <summary>
    /// 标记Id周围的格子 雷的数量
    /// </summary>
    /// <param name="_id"></param>
    void FlagAround(int _id)
    {

        int x_id = _id % Width;
        int y_id = _id / Width;

        for(int i=0; i<8; i++)
        {
            int _x = x_id + Around_X_Gen[i];
            int _y = y_id + Around_Y_Gen[i];

            if (Between(_x, 0, Width) && Between(_y, 0, Height))
            {
                int id = _y * Width + _x;                
                if(cells[id].isLandmine == false)
                {
                    cells[id].Number++;
                }
            }
        }

    }

    /// <summary>
    /// 显示所有格子
    /// </summary>
    void ShowAll()
    {
        foreach (var cell in cells)
        {
            cell.isShow = true;
        }
    }

    /// <summary>
    /// 游戏开始
    /// </summary>
    public void GameStart()
    {
        audioData.StartBgm();

        foreach (var cell in cells)
        {
            cell.ResetCell();
            cell.transform.DOScale(0, 0.2f);
        }

        isWin = false;
        ShowNumber = 0;

        this.DelayToDo(0.3f, ()=> {
            Genarate();

            foreach (var cell in cells)
            {
                cell.transform.DOScale(1, 0.2f);
            }
        });
        
    }

    /// <summary>
    /// 检查从这个ID开始遍历
    /// </summary>
    /// <param name="_id"></param>
    public void CheckCell(int _id)
    {
        if(cells[_id].isLandmine)
        {
            audioData.Boom();

            ShowAll();
            isWin = false;

            audioData.StopBgm();
            UIManager.instance.OpenUI<UIGameRet>(this);
            return;
        }

        audioData.Click();

        CheckAround(_id, true);

        if (ShowNumber + LandmineNumber==AllNumber)
        {
            ShowAll();
            isWin = true;

            audioData.StopBgm();
            UIManager.instance.OpenUI<UIGameRet>(this);
            return;
        }
    }

    /// <summary>
    /// 检查这个Id周围的八个格子
    /// </summary>
    /// <param name="_id"></param>
    /// <param name="isFirst">是否为第一个也就是点击的格子</param>
    void CheckAround(int _id, bool isFirst=false)
    {
        if (cells[_id].Show() && !isFirst) return;

        int x_id = _id % Width;
        int y_id = _id / Width;

        for (int i = 0; i < 8; i++)
        {
            int _x = x_id + Around_X_Gen[i];
            int _y = y_id + Around_Y_Gen[i];

            
            if (Between(_x, 0, Width) && Between(_y, 0, Height))
            {
                int id = _y * Width + _x;
                if(cells[id].CanShow())
                {
                    CheckAround(id);
                }
            }
        }
    }

    bool Between(int v, int _left, int _right)
    {
        return v >= _left && v < _right;
    }

    
}
