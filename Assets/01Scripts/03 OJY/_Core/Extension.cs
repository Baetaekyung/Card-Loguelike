using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CardGame
{
    public static class Extension
    {
        public static void AutoDelegate(this MonoBehaviour monoBehaviour, Delegate targetDelegate, Delegate subscribeDelegate)
        {

        }
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
}
