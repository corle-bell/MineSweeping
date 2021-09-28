using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BmFramework.Core
{
    public class UIAnimationRoot : UIRoot
    {
        public UIAnimationRes uIAnimationRes;
        public override void OnOpen(object _data=null)
        {
            base.OnOpen(_data);

            //do open animtion here

            uIAnimationRes.Open();
        }

        public virtual void OnOpenAnimation()
        {
            //on open animtion start
        }

        public virtual void OnOpenAnimationEnd()
        {
            //on open animtion end
        }

        public virtual void OnCloseAnimation()
        {
            //on close animtion start
        }

        public virtual void OnCloseAnimationEnd()
        {
            RemoveFormCanvas();

            //on close animtion end
        }

        public override void OnClose()
        {
            //do close animtion here

            uIAnimationRes.Close();
        }

    }
}

