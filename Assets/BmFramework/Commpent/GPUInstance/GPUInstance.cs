using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BmFramework.Core.Rendering
{
    public class GPUInstance : MonoBehaviour
    {
        public static GPUInstance I;

        public List<GPUInstance_Painter> painters = new List<GPUInstance_Painter>();
        private void Awake()
        {
            if(I==null)
            {
                I = this;
                DontDestroyOnLoad(gameObject);
            }

        }

        private void LateUpdate()
        {
            for(int i=0; i< painters.Count; i++)
            {
                painters[i].Logic();
            }
        }

        #region Public Fun
        public void AddPainter(GPUInstance_Painter _Painter)
        {
            painters.Add(_Painter);
        }

        public void RemovePainter(GPUInstance_Painter _Painter)
        {
            painters.Remove(_Painter);
        }
        #endregion
    }
}


