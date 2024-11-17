using CardGame.GameEvent;
using System.Collections.Generic;
using UnityEngine;

namespace CardGame
{
    public class AudioEmitterRandom : MonoBehaviour
    {
        [SerializeField] private List<AudioClip> audioClips;
        [SerializeField] private AudioSource audioSource;
        public void PlayAudio()
        {
            int randomIndx = Random.Range(0, audioClips.Count);
            AudioClip randomClip = audioClips[randomIndx];
            audioSource.PlayOneShot(randomClip);
        }
    }
}
