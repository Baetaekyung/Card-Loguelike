using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class Extension
{
    public static T GetOrAddComponent<T>(this MonoBehaviour monoBehaviour) where T : Component
    {
        T result = monoBehaviour.gameObject.GetComponent<T>();
        if (result == null)
        {
            Debug.LogWarning("[WARN]_Can't find component, add new one...");
            result = monoBehaviour.gameObject.AddComponent<T>();
        }
        return result;
    }

}