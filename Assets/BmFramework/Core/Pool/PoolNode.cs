using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BmFramework.Core
{
    public class PoolNode:MonoBehaviour
    {
        [EnumName("路径")]
        public string path;

        [EnumName("生存时间")]
        public float live=3.0f;

        public PoolManager manager;

        public void Acitve()
        {
            gameObject.SetActive(true);

            if(live>0) this.DelayToDo(live, this.Recycle);
        }

        public virtual void Recycle()
        {
            StopAllCoroutines();
            gameObject.SetActive(false);
            manager.Recycle(this);
        }
    }
}

