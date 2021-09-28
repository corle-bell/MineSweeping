using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BmFramework.Core
{
    public class FrameworkMain : MonoBehaviour
    {
        public static FrameworkMain instance = null;


        [EnumName("设计分辨率")]
        public Vector2 screenSize = new Vector2(1080, 1920);
        [EnumName("Task管理器轮询间隔(S)")]
        public float task_tick=0.15f;
        [EnumName("音效播放组件引用池大小")]
        public int sound_pool_max = 50;
        [EnumName("是否在Dev模式")]
        public bool isDev=true;

        UIKit uiKit;
        DataKit dataKit;
        AudioKit audioKit;
        PoolKit poolKit;
        EventKit eventKit;
        TaskKit taskKit;
        ResourceKit resourceKit;

        List<FrameworKit> kitArr = new List<FrameworKit>();
        void Awake()
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            //初始化日志
            BmDebug.Init();

            uiKit = new UIKit();
            uiKit.OnInit();
            kitArr.Add(uiKit);

            dataKit = new DataKit();
            dataKit.OnInit();
            kitArr.Add(dataKit);

            audioKit = new AudioKit();
            audioKit.OnInit();
            kitArr.Add(audioKit);

            poolKit = new PoolKit();
            poolKit.OnInit();
            kitArr.Add(poolKit);

            eventKit = new EventKit();
            eventKit.OnInit();
            kitArr.Add(eventKit);

            taskKit = new TaskKit();
            taskKit.OnInit();
            kitArr.Add(taskKit);

            resourceKit = new ResourceKit();
            resourceKit.OnInit();
            kitArr.Add(resourceKit);
        }

        // Update is called once per frame
        void Update()
        {
            foreach(var item in kitArr)
            {
                item.OnUpdate(Time.unscaledDeltaTime);
            }
        }
    }
}
