namespace CardGame
{
    //unfinished
    public static partial class Game
    {
        public abstract class BaseEvent : System.IComparable<BaseEvent>
        {
            public float delay;

            public int CompareTo(BaseEvent other)
            {
                return delay.CompareTo(other.delay);
            }
            public abstract void Execute();
            public virtual void TryInvoke() => Execute();
            public virtual void Dispose()
            {
            }
        }
        public abstract class Event<T> : BaseEvent where T : Event<T>
        {
            public static System.Action<T> OnExecute;

            public override void TryInvoke()
            {
                Execute();
                OnExecute?.Invoke((T)this);
            }
        }
    }
}
