using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BmFramework.Core;
public static class MonoBehaviourExtensionMethods
{
    public static void DelayToDo(this MonoBehaviour _script, float _delay, Action _action)
    {
        if(_script.gameObject.activeInHierarchy)
        {
            _script.StartCoroutine(DelayToCall(_delay, _action));
        }
        else
        {
            SequenceAction.Create().Delay(_delay, _action).Execute();
        }
    }

    static IEnumerator DelayToCall(float _delay, Action action)
    {
        yield return new WaitForSecondsRealtime(_delay);
        action?.Invoke();
    }

    public static void AddComponent(this MonoBehaviour _script, string _type)
    {
        System.Type t = System.Type.GetType(_type);
        if(t!=null)_script.gameObject.AddComponent(t);
    }

    public static void AddComponentByString(this GameObject _script, string _type)
    {
        System.Type t = System.Type.GetType(_type);
        if (t != null) _script.gameObject.AddComponent(t);
    }

    public static void SetActive(this MonoBehaviour _script, bool _isActive)
    {
        _script.gameObject.SetActive(_isActive);
    }

}
