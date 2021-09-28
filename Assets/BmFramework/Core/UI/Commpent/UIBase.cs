using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BmFramework.Core
{
    public enum UITouchType
    {
        Press,
        Up,
        Click,
    }

    public class UIBase : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
    {
        public bool isTouched;

        private void Awake()
        {
            OnInit();
        }

        public virtual void SetVisible(bool _true)
        {
            gameObject.SetActive(_true);
        }

        public virtual void OnInit()
        {

        }


        public virtual void OnLogic()
        {

        }

        protected virtual void OnTouchEvent(UITouchType _type, PointerEventData eventData)
        {

        }

        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            this.OnTouchEvent(UITouchType.Click, eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            isTouched = false;
            this.OnTouchEvent(UITouchType.Up, eventData);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            isTouched = true;
            this.OnTouchEvent(UITouchType.Press, eventData);
        }
    }
}

