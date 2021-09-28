using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BmFramework.Core{
    public abstract class UIAnimationRes: MonoBehaviour
    {
        public UIAnimationRoot root;
        public abstract void Open();
        public abstract void Close();
    }
}
