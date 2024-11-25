using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace CardGame
{
    public class VolumeSliders : MonoBehaviour
    {
        [SerializeField] private AudioMixer _audioMixer;

        [SerializeField] private Slider _bgmSlider;
        [SerializeField] private Slider _sfxSlider;

        private void Awake()
        {
            _bgmSlider.onValueChanged.AddListener(HandleBgmSliderChanged);
            _sfxSlider.onValueChanged.AddListener(HandleSfxSliderChanged);

            _audioMixer.SetFloat("SFX", 0);
            _audioMixer.SetFloat("BGM", 0);
        }

        private void HandleSfxSliderChanged(float volume)
        {
            _audioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        }

        private void HandleBgmSliderChanged(float volume)
        {
            _audioMixer.SetFloat("BGM", Mathf.Log10(volume) * 20);
        }
    }
}
