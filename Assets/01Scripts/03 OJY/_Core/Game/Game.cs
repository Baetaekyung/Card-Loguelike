using System;
using System.Collections.Generic;
using UnityEngine;
namespace CardGame
{
    //unfinished
    public static partial class Game
    {
        private readonly static Queue<BaseEvent> eventQueue = new();
        private readonly static Dictionary<Type, Stack<BaseEvent>> eventPools = new();

        public static T New<T>() where T : BaseEvent, new()
        {
            if (!eventPools.TryGetValue(typeof(T), out Stack<BaseEvent> pool))
            {
                pool = new Stack<BaseEvent>(4);
                pool.Push(new T());
                eventPools[typeof(T)] = pool;
            }
            if (pool.Count > 0)
                return (T)pool.Pop();
            else
                return new T();
        }

        // Clear all pending events and reset the tick to 0.
        public static void Clear()
        {
            eventQueue.Clear();
        }
        public static T Schedule<T>(float tick = 0) where T : BaseEvent, new()
        {
            var ev = New<T>();
            ev.delay = Time.time + tick;
            eventQueue.Enqueue(ev);
            return ev;
        }

        /// <summary>
        /// Tick the simulation. Returns the count of remaining events.
        /// If remaining events is zero, the simulation is finished unless events are
        /// injected from an external system via a Schedule() call.
        /// </summary>
        /// <returns></returns>
        public static void Tick()
        {
            var time = Time.time;
            while (eventQueue.Count > 0 && eventQueue.Peek().delay <= time)
            {
                var ev = eventQueue.Dequeue();
                var tick = ev.delay;
                ev.TryInvoke();
                if (ev.delay > tick)
                {
                    //event was rescheduled, so do not return it to the pool.
                }
                else
                {
                    // Debug.Log($"<color=green>{ev.tick} {ev.GetType().Name}</color>");
                    ev.Dispose();
                    try
                    {
                        eventPools[ev.GetType()].Push(ev);
                    }
                    catch (KeyNotFoundException)
                    {
                        //This really should never happen inside a production build.
                        Debug.LogError($"No Pool for: {ev.GetType()}");
                    }
                }
            }
        }

    }
}
