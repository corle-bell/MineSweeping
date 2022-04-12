using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LifeGame : MonoBehaviour
{
    public LifeNode[] nodes;
    public int row = 22;
    int[] neighbor;
    int[] neighborLine;
    int[] randomData;
    public int status = 0;
    [Range(10, 30)]
    public int randomParams;
    public float updateTime = 0.1f;
    float tick = 0;
    // Start is called before the first frame update
    void Awake()
    {
        nodes = GetComponentsInChildren<LifeNode>();
        neighbor = new int[] { -row - 1, -row, -row + 1, -1, +1, row - 1, row, row + 1 };
        neighborLine = new int[] { 1, 1, 1, 0, 0, -1, -1, -1 };
        randomData = new int[nodes.Length];
    }


    void RandomGrids()
    {
        randomData.FillByIndex();

        int randomNumber = row* randomParams;
        int len = randomData.Length;
        for (int i = 0; i< randomNumber; i++)
        {
            int id = FrameworkTools.Ranomd(len);
            int s_id = randomData[id];
            len--;
            randomData.Swap(id, len);
            nodes[s_id].SetState(1);
            nodes[s_id].Record();
        }

        for(int i=0; i<len; i++)
        {
            nodes[randomData[i]].SetState(0);
            nodes[randomData[i]].Record();
        }
    }

    void Clean()
    {
        for (int i = 0; i < nodes.Length; i++)
        {
            nodes[i].SetState(0);
            nodes[i].Record();
        }
        tick = 0;
    }

    void Logic()
    {
        for (int i = 0; i < nodes.Length; i++)
        {
            CheckOne(i);
        }
        for (int i = 0; i < nodes.Length; i++)
        {
            nodes[i].Record();
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch(status)
        {
            case 1:
                tick += Time.deltaTime;
                if(tick>=updateTime)
                {
                    tick = 0;
                    Logic();
                }
                break;
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            status = 0;
            RandomGrids();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            status = 0;
            Clean();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            status = status==1?0:1;
            tick = 0;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Logic();
        }
    }

    void CheckOne(int id)
    {
        int neighborLiveNum = 0;
        for(int i=0; i< neighbor.Length; i++)
        {
            int n_id = id + neighbor[i];
            bool isLine = (n_id/row + neighborLine[i]) == id/row;
            neighborLiveNum += (n_id >= 0 && n_id < nodes.Length && isLine) ? nodes[n_id].lastState : 0;
        }
        if (nodes[id].state==1)
        {
            nodes[id].SetState(neighborLiveNum >= 2 && neighborLiveNum < 4 ? 1 : 0);
        }
        else
        {
            nodes[id].SetState(neighborLiveNum ==3  ? 1 : 0);
        }
    }


}
