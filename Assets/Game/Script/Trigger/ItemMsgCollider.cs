using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using BmFramework.Core;
public class ItemMsgCollider : MonoBehaviour
{
    [EnumName("类型:")]
    public NotifyType type;
    public string _cmd;
    public bool isOnce;
    public bool isHit = false;
    public bool isHideAfterTrigger;

    private void OnCollisionEnter(Collision collision)
    {
        if(isOnce)
        {
            if (isHit) return;
            isHit = true;
        }
        NotifacitionManager.Post(type, collision, _cmd);
        if (isHideAfterTrigger) gameObject.SetActive(false);
    }

}
