using UnityEngine;

namespace CardGame
{

    public class WaveManager : MonoSingleton<WaveManager>
    {
        public int CurrentWave { get; private set; }

    }
}
