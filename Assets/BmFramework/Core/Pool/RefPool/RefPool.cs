using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BmFramework.Core
{
    public sealed class RefPool
    {
        public int RefUseCount;
        public int MaxCount;

        private Queue<IRef> refQueue = new Queue<IRef>();

        public override string ToString()
        {
            return  string.Format("Ref Count:{0}  Queue Count:{1}", RefUseCount, refQueue.Count);
        }

        public int GetRefCount()
        {
            return refQueue.Count;
        }

        public RefPool(int _max)
        {
            MaxCount = _max;
        }

        public T Get<T>() where T : class, IRef, new()
        {
            T _ref;
            if (refQueue.Count > 0)
            {
                _ref = refQueue.Dequeue() as T;
                RefUseCount++;
            }
            else if(RefUseCount + refQueue.Count < MaxCount)
            {
                _ref = new T();
                RefUseCount++;
            }
            else
            {
                _ref = new T();
            }
            return _ref;
        }

        public void Add(IRef _ref)
        {
            if (RefUseCount + refQueue.Count < MaxCount)
            {
                refQueue.Enqueue(_ref);
            }
        }

        public void Recycle(IRef _ref)
        {
            _ref.Reset();
            if (RefUseCount + refQueue.Count < MaxCount)
            {
                refQueue.Enqueue(_ref);
                RefUseCount--;
            }
            else
            {
                _ref.Destroy();
                _ref = null;
            }
        }
    }

}

