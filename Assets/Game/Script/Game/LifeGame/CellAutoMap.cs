using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CellAutoMap : MonoBehaviour
{
    public LifeNode[] nodes;
    public int row = 22;
    int[] neighbor;
    int[] neighborLine;
    int[] randomData;
    public int status = 0;
    [Range(10, 100)]
    public int randomParams;
    public float updateTime = 0.1f;
    float tick = 0;

    [Range(0, 1.0f)]
    public float perlinParams;

    public float space = 5f;
    public float tile_size = 25;

    public int loopCount;

    private LifeNode first;
    private int leftCount;
    private int startIndex;
    private Vector2 startPos;
    private Vector2 size;
    // Start is called before the first frame update
    void Awake()
    {
        nodes = GetComponentsInChildren<LifeNode>();
        first = nodes[0];
        neighbor = new int[] { -row - 1, -row, -row + 1, -1, +1, row - 1, row, row + 1 };
        neighborLine = new int[] { 1, 1, 1, 0, 0, -1, -1, -1 };

        float itemSize = tile_size + space;
        float itemSizeHalf = itemSize / 2;

        size = new Vector2(row* tile_size, row * tile_size);
        startPos = new Vector2(-row * itemSizeHalf - (itemSizeHalf+space), -row * itemSizeHalf - -(itemSizeHalf + space));

        for(int i=0; i< nodes.Length; i++)
        {
            int x = i % row;
            int y = i / row;
            var trans = nodes[i].transform as RectTransform;
            trans.sizeDelta = Vector2.one * tile_size;
            trans.anchoredPosition = startPos + new Vector2(x * itemSize, y * itemSize);
        }
    }

    private void Start()
    {
        CreateNode();
    }

    void CreateNode()
    {
        int count = row * row;
        leftCount = count - nodes.Length;
        startIndex = nodes.Length;
        System.Array.Resize(ref nodes, count);

        status = 99;

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

    void PerlinNoiseGrids()
    {
        
        for (int i = 0; i < nodes.Length; i++)
        {
            int x = i % row;
            int y = i / row;

            float v =  Mathf.PerlinNoise(((float)x)/row*(FrameworkTools.Ranomd(20, 25f)), ((float)y) / row * (FrameworkTools.Ranomd(20, 25f)));
            nodes[i].SetState(v>= perlinParams ? 1 : 0); //噪声图分界

            nodes[i].Record();

            //nodes[i].image.color = Color.Lerp(Color.white, Color.green, v);  //绘制噪声图像
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
        loopCount++;
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
            case 99:
                {
                    int len = leftCount >= row ? row : leftCount;
                    for(int i=0; i<len; i++)
                    {
                        int x = startIndex % row;
                        int y = startIndex / row;
                        
                        var t = nodes[startIndex] = GameObject.Instantiate(first.gameObject).GetComponent<LifeNode>();
                        t.transform.parent = transform;
                        (t.transform as RectTransform).sizeDelta = Vector2.one * tile_size;
                        (t.transform as RectTransform).anchoredPosition = startPos + new Vector2(x * (tile_size + space), y * (tile_size + space));
                        startIndex++;
                    }
                    leftCount -= len;
                    status = leftCount == 0 ? 0 : 99;
                }
                break;
        }

        if(status!=99)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                status = 0;
                RandomGrids();
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                status = 0;
                PerlinNoiseGrids();
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                status = 0;
                Clean();
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if(status==0)
                {
                    loopCount = 0;
                }
                status = status == 1 ? 0 : 1;
                tick = 0;
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                Logic();
            }
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

        if(neighborLiveNum != 4)
        {
            nodes[id].SetState(neighborLiveNum > 4 ? 1 : 0);
        }
    }


}
