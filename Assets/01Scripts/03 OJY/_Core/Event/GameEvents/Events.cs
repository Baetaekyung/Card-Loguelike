using System;
using UnityEngine;

//unfinished
namespace CardGame.GameEvent
{
    public static class Events<T> where T : class, new()
    {
        public static T Instance { get; } = CSharpAutoSingleton<T>.instance;
    }

    public abstract class BaseEvent
    {
        public virtual void Invoke()
        {
            //Debug.LogWarning("dont call me");
        }
    }
    public class EventCameraShake : BaseEvent
    {
        public float impulse;
    }
    public class EventOnEnemyHit : BaseEvent
    {
        
    }
    public class EventPlayerHirt : BaseEvent
    {

    }
}
