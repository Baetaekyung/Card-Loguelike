using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CardGame
{
    public class GameEvent
    {
        public static class Events
        {
            //public static ObjectiveUpdateEvent ObjectiveUpdateEvent = new ObjectiveUpdateEvent();
            //public static AllObjectivesCompletedEvent AllObjectivesCompletedEvent = new AllObjectivesCompletedEvent();
            //public static GameOverEvent GameOverEvent = new GameOverEvent();
            //public static PlayerDeathEvent PlayerDeathEvent = new PlayerDeathEvent();
            //public static EnemyKillEvent EnemyKillEvent = new EnemyKillEvent();
            //public static PickupEvent PickupEvent = new PickupEvent();
            //public static AmmoPickupEvent AmmoPickupEvent = new AmmoPickupEvent();
            //public static DamageEvent DamageEvent = new DamageEvent();
            //public static DisplayMessageEvent DisplayMessageEvent = new DisplayMessageEvent();
        }

        public class ObjectiveUpdateEvent : GameEvent
        {
            public string DescriptionText;
            public string CounterText;
            public bool IsComplete;
            public string NotificationText;
        }

        public class AllObjectivesCompletedEvent : GameEvent { }

        public class GameOverEvent : GameEvent
        {
            public bool Win;
        }

        public class PlayerDeathEvent : GameEvent { }

        public class EnemyKillEvent : GameEvent
        {
            public GameObject Enemy;
            public int RemainingEnemyCount;
        }

        public class PickupEvent : GameEvent
        {
            public GameObject Pickup;
        }

        public class AmmoPickupEvent : GameEvent
        {
        }

        public class DamageEvent : GameEvent
        {
            public GameObject Sender;
            public float DamageValue;
        }

        public class DisplayMessageEvent : GameEvent
        {
            public string Message;
            public float DelayBeforeDisplay;
        }
    }
    public static class EventManager
    {
        static readonly Dictionary<Type, Action<GameEvent>> s_Events = new();

        static readonly Dictionary<Delegate, Action<GameEvent>> s_EventLookups = new();

        public static void AddListener<T>(Action<T> evt) where T : GameEvent
        {
            if (!s_EventLookups.ContainsKey(evt))
            {
                void newAction(GameEvent e) => evt((T)e);
                s_EventLookups[evt] = newAction;

                if (s_Events.TryGetValue(typeof(T), out Action<GameEvent> internalAction))
                    s_Events[typeof(T)] = internalAction += newAction;
                else
                    s_Events[typeof(T)] = newAction;
            }
        }

        public static void RemoveListener<T>(Action<T> evt) where T : GameEvent
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

        public static void Broadcast(GameEvent evt)
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
