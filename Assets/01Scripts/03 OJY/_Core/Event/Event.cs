using System;

//unfinished
namespace CardGame.MyEvents
{
    public abstract class BaseEvent
    {
        protected abstract void Invoke();
        public virtual void TryInvoke() => Invoke();
    }
    public abstract class Event<T> : BaseEvent where T : Event<T>
    {
        public static Action<T> broadcast;

        public override void TryInvoke()
        {
            Invoke();
            broadcast?.Invoke(this as T);
        }
    }
}
