using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemStayEventTrigger : MonoBehaviour
{
    public string tagFilter = "";
    public UnityEvent[] eventArr;
    public float stayTime=1.0f;
    public bool isOnce;
    public bool isHideAfterTrigger;
    Collider collider;

    float timestamp = 0;
    private void Awake()
    {
        collider = GetComponent<Collider>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (eventArr == null || (!string.IsNullOrEmpty(tagFilter) && !other.CompareTag(tagFilter))) return;
        timestamp = Time.realtimeSinceStartup;
    }

    private void OnTriggerStay(Collider other)
    {
        if(Time.realtimeSinceStartup-timestamp >= stayTime)
        {
            Call();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
    }

    private void Call()
    {
        foreach (var item in eventArr)
        {
            item.Invoke();
        }
        if (isOnce) collider.enabled = false;
        if (isHideAfterTrigger) gameObject.SetActive(false);
    }

}
