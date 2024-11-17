using CardGame.GameEvent;
using UnityEngine;

namespace CardGame
{
    public class AudioEmitter : MonoBehaviour
    {
        [SerializeField] private AudioClip audioClip;
        [SerializeField] private AudioSource audioSource;
        public void PlayAudio()
        {
            audioSource.PlayOneShot(audioClip);
        }
    }
}
