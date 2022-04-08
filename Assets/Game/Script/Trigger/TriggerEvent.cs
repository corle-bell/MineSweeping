using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITriggerReciver
{
    void IOnTriggerEnter(Collider other);
    void IOnTriggerStay(Collider other);
    void IOnTriggerExit(Collider other);
}

public class TriggerEvent : MonoBehaviour
{
    public ITriggerReciver reciver;
    private void OnTriggerEnter(Collider other)
    {
        reciver?.IOnTriggerEnter(other);
    }

    private void OnTriggerStay(Collider other)
    {
        reciver?.IOnTriggerStay(other);
    }

    private void OnTriggerExit(Collider other)
    {
        reciver?.IOnTriggerExit(other);
    }
}
