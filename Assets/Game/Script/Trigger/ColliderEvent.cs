using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IColliderEventReciver
{
    void IOnCollisionEnter(Collision other, GameObject sender);
    void IOnCollisionStay(Collision other, GameObject sender);
    void IOnCollisionExit(Collision other, GameObject sender);
}

public class ColliderEvent : MonoBehaviour
{
    public IColliderEventReciver reciver;
    private void OnCollisionEnter(Collision collision)
    {
        reciver?.IOnCollisionEnter(collision, gameObject);
    }

    private void OnCollisionStay(Collision collision)
    {
        reciver?.IOnCollisionStay(collision, gameObject);
    }

    private void OnCollisionExit(Collision collision)
    {
        reciver?.IOnCollisionExit(collision, gameObject);
    }
    
}
