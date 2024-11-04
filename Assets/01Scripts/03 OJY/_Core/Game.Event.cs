namespace CardGame
{
    public static partial class Game
    {
        public abstract class Event : System.IComparable<Event>
        {
            public float delay;

            public int CompareTo(Event other)
            {
                return delay.CompareTo(other.delay);
            }
            public abstract void Execute();
            public virtual void TryInvoke() => Execute();
            public virtual void Dispose()
            {
            }
        }
        public abstract class Event<T> : Event where T : Event<T>
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
