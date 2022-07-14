using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BmFramework.Core;
public static class MonoBehaviourExtensionMethods
{
    public static void DelayToDo(this MonoBehaviour _script, float _delay, Action _action, bool isTimeScale=true)
    {
        if(_script.gameObject.activeInHierarchy)
        {
            _script.StartCoroutine(DelayToCall(_delay, _action, isTimeScale));
        }
        else
        {
            SequenceAction.Create().Delay(_delay, _action).Execute();
        }
    }

    static IEnumerator DelayToCall(float _delay, Action action, bool isTimeScale)
    {
        if(isTimeScale)
        {
            yield return new WaitForSeconds(_delay);
        }
        else
        {
            yield return new WaitForSecondsRealtime(_delay);
        }
        
        action?.Invoke();
    }

    public static void WaitForEndFrame(this MonoBehaviour _script, Action _action)
    {
        if (_script.gameObject.activeInHierarchy)
        {
            _script.StartCoroutine(WaitForEndFrame(_action));
        }
    }

    static IEnumerator WaitForEndFrame(Action action)
    {
        yield return new WaitForEndOfFrame();
        action?.Invoke();
    }

    public static void AddComponent(this MonoBehaviour _script, string _type)
    {
        System.Type t = System.Type.GetType(_type);
        if(t!=null)_script.gameObject.AddComponent(t);
    }

    public static Component AddComponentByString(this GameObject _script, string _type)
    {
        System.Type t = System.Type.GetType(_type);
        if (t != null) 
           return _script.gameObject.AddComponent(t);
        return null;
    }

    public static void SetActive(this MonoBehaviour _script, bool _isActive)
    {
        _script.gameObject.SetActive(_isActive);
    }

    public static void SetPosition(this MonoBehaviour _script, Vector3 pos)
    {
        _script.transform.position = pos;
    }

    public static T[] FindChildComponentsByName<T>(this MonoBehaviour _script, string _RootName)
    {
        return _script.transform.FindChildDeep(_RootName).GetComponentsInChildren<T>();
    }

    public static T[] FindComponentsByName<T>(this MonoBehaviour _script, string _RootName)
    {
        return _script.transform.FindChildDeep(_RootName).GetComponents<T>();
    }

    public static T FindChildComponentByName<T>(this MonoBehaviour _script, string _RootName)
    {
        return _script.transform.FindChildDeep(_RootName).GetComponent<T>();
    }
}
