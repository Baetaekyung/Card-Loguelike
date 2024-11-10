using System;
using UnityEngine;

namespace CardGame.Events
{
    public static class MyEventManager
    {
        public static Action<MyEvent> Act;
        public static void Invoke(MyEvent ins)
        {
            Act?.Invoke(ins);
        }
    }
    public class MyEvent
    {
        public TestMono2 instance;

    }
}
