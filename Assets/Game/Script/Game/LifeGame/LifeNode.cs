using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class LifeNode : MonoBehaviour , IPointerDownHandler
{
    public Image image;
    public int state;
    public int lastState;
    public void SetState(int _state)
    {
        state = _state;
        image.color = state == 1 ? Color.green : Color.white;
    }
    public void Record()
    {
        lastState = state;
    }
    private void Reset()
    {
        image = GetComponent<Image>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        SetState(state == 1 ? 0 : 1);
        Record();
    }
}
