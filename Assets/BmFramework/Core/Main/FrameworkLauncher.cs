using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BmFramework.Core
{
    public class FrameworkLauncher : MonoBehaviour
    {
        [EnumName("启动后的场景")]
        public string LauncheScene;

        [EnumName("启动后的UI")]
        public string LauncheUI;


        // Start is called before the first frame update
        void Start()
        {
            GameStart();
        }

        public virtual void GameStart()
        {
            if(LauncheUI!="")
            {
                UIManager.instance.OpenUI(LauncheUI);
            }
            else
            {
                SceneHelper.LoadScene(LauncheScene);
            }
        }
    }
}

