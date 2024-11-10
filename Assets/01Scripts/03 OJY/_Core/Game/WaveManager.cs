using UnityEngine;

namespace CardGame
{
    public enum WaveState
    {

    }

    public class WaveManager : MonoSingleton<WaveManager>
    {
        public int CurrentWave { get; private set; }

    }
}
