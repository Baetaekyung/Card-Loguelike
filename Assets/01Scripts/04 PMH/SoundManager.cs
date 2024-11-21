using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public enum SfxType
{
    BGM = 0, Hit = 1, Melee = 2, bow = 3, Select = 4
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public AudioMixer audioMixer;
    public AudioMixerGroup MasterGroup;
    public AudioMixerGroup BGMGroup;
    public AudioMixerGroup SFXGroup;
    //하는중

    private readonly string sound_master = "Master";
    private readonly string sound_bgm = "BGM";
    private readonly string sound_sfx = "SFX";

    [Header("BGM")]
    public AudioClip bgmClip;
    public float bgmVolume;

    private AudioSource bgmPlayer;

    [Header("SFX")]
    public SFXSO[] sfxClipSO;
    public float sfxVolum;
    public int channels;

    private AudioSource[] _sfxPlayers;
    private int _channelIndex;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        Init();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            int ran = UnityEngine.Random.Range(0, sfxClipSO.Length);
            if (ran == 0)
            {
                PlayerSFX(SfxType.Hit);
            }
            else if (ran == 1)
            {
                PlayerSFX(SfxType.Melee);
            }
            else if (ran == 2)
            {
                PlayerSFX(SfxType.bow);
            }
            else
            {
                PlayerSFX(SfxType.Select);
            }
        }
    }

    private void Init()
    {
        //배경음 초기화
        GameObject bgmObject = new GameObject("BgmPlayers");
        bgmObject.transform.parent = transform;
        bgmPlayer = bgmObject.AddComponent<AudioSource>();
        bgmPlayer.playOnAwake = false;
        bgmPlayer.loop = true;
        bgmPlayer.volume = bgmVolume;
        bgmPlayer.clip = bgmClip;

        //효과음 플레이어 초기화
        GameObject sfxObject = new GameObject("SfxPlayer");
        sfxObject.transform.parent = transform;
        _sfxPlayers = new AudioSource[channels];

        for(int i = 0; i<_sfxPlayers.Length; i++)
        {
            _sfxPlayers[i] = sfxObject.AddComponent<AudioSource>();
            _sfxPlayers[i].playOnAwake = false;
            _sfxPlayers[i].volume = sfxVolum;
        }
    }
    public void PlayerSFX(SfxType sfxType)
    {
        for(int i = 0; i < _sfxPlayers.Length; i++)
        {
            int loopIndex = (i + _channelIndex) % _sfxPlayers.Length;
            
            if(_sfxPlayers[loopIndex].isPlaying)
            {
                continue;
            }
            _channelIndex = loopIndex;

            foreach(var item in sfxClipSO)
            {
                if (item.sfxType == sfxType) _sfxPlayers[loopIndex].clip = item.audioClip;
                _sfxPlayers[loopIndex].outputAudioMixerGroup = audioMixer.outputAudioMixerGroup;

                if(sfxType == SfxType.BGM)
                {
                    _sfxPlayers[loopIndex].outputAudioMixerGroup = BGMGroup;
                }
                else
                {
                    _sfxPlayers[loopIndex].outputAudioMixerGroup = SFXGroup;
                }
                
            }

            _sfxPlayers[loopIndex].Play();
            break;
        }
    }
}
