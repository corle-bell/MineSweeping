using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class ItemEventCall : MonoBehaviour
{
    public UnityEvent call;
    public void Do()
    {
        call?.Invoke();
    }
}
