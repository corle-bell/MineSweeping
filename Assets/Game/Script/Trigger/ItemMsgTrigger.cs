using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using BmFramework.Core;
public class ItemMsgTrigger : MonoBehaviour
{
    [EnumName("类型:")]
    public NotifyType type;
    public string _cmd;
    public bool isOnce;
    public bool isHideAfterTrigger;
    Collider collider;
    private void Awake()
    {
        collider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        NotifacitionManager.Post(type, this, _cmd);
        if (isOnce) collider.enabled = false;
        if (isHideAfterTrigger) gameObject.SetActive(false);        
    }

}
