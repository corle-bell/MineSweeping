using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BmFramework.Core
{
    public class UIRoot : UIBase
    {
        [HideInInspector]
        public List<UIBase> uiComments = new List<UIBase>();
        public bool isSingle = true;
        bool isDestory;
        public override void OnInit()
        {
            base.OnInit();

            UIBase[] childs = gameObject.GetComponentsInChildren<UIBase>(true);
            foreach (var childUI in childs)
            {
                if(childUI.GetInstanceID()!=GetInstanceID())
                uiComments.Add(childUI);
            }
        }

        public virtual void OnOpen(object _data=null)
        {

        }

        public override void OnLogic()
        {
            foreach(var item in uiComments)
            {
                item.OnLogic();
            }
        }

        

        protected void RemoveFormCanvas()
        {
            if (isDestory) return;
            isDestory = true;
            transform.parent = null;
            Destroy(gameObject);
        }

        public virtual void OnClose()
        {
            RemoveFormCanvas();
        }

        public void Close()
        {
            UIManager.instance.CloseUI(this);
        }
    }
}

