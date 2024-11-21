using CardGame.GameEvent;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CardGame
{
    public class TimeManager : MonoSingleton<TimeManager>
    {
        private static Coroutine coroutineSlowMotion;
        public void Slowmotion(float delay, float value)
        {
            if (coroutineSlowMotion != null) StopCoroutine(coroutineSlowMotion);
            coroutineSlowMotion = StartCoroutine(SlowMotionCoroutine());
            IEnumerator SlowMotionCoroutine()
            {
                print("slowmotion");
                Time.timeScale = value;
                float timer = 0;
                while (timer <= delay)
                {
                    timer += Time.unscaledDeltaTime;
                    yield return null;
                }
                Time.timeScale = 1;
            }
        }
    }
}