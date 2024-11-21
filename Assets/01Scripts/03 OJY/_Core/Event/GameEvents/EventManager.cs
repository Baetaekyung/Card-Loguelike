using System;
using System.Collections.Generic;
using UnityEngine;

namespace CardGame.GameEvent
{
    public static class EventManager
    {
        public static readonly Dictionary<Type, Action<BaseEvent>> s_Events = new();
        public static readonly Dictionary<Delegate, Action<BaseEvent>> s_EventLookups = new();

        public static void AddListener<T>(Action<T> evt) where T : BaseEvent
        {
            if (!s_EventLookups.ContainsKey(evt))
            {
                void newAction(BaseEvent e)
                {
                    evt(e as T);
                    Debug.Log("invokeshit");
                    e.Invoke();
                }

                s_EventLookups[evt] = newAction;

                if (s_Events.TryGetValue(typeof(T), out Action<BaseEvent> internalAction))
                    s_Events[typeof(T)] = internalAction += newAction;
                else
                    s_Events[typeof(T)] = newAction;
            }
        }

        public static void RemoveListener<T>(Action<T> evt) where T : BaseEvent
        {
            if (s_EventLookups.TryGetValue(evt, out var action))
            {
                if (s_Events.TryGetValue(typeof(T), out var tempAction))
                {
                    tempAction -= action;
                    if (tempAction == null)
                        s_Events.Remove(typeof(T));
                    else
                        s_Events[typeof(T)] = tempAction;
                }

                s_EventLookups.Remove(evt);
            }
        }

        public static void Invoke(BaseEvent evt)
        {
            if (s_Events.TryGetValue(evt.GetType(), out var action))
                action.Invoke(evt);
        }

        public static void Clear()
        {
            s_Events.Clear();
            s_EventLookups.Clear();
        }
    }
}
