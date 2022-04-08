using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemEventTrigger : MonoBehaviour
{
    public string tagFilter = "";
    public UnityEvent[] eventArr;

    public bool isOnce;
    public bool isHideAfterTrigger;
    Collider collider;
    private void Awake()
    {
        collider = GetComponent<Collider>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (eventArr == null || (!string.IsNullOrEmpty(tagFilter) && !other.CompareTag(tagFilter))) return;
        foreach(var item in eventArr)
        {
            item.Invoke();
        }
        if (isOnce) collider.enabled = false;
        if (isHideAfterTrigger) gameObject.SetActive(false);
    }

}
