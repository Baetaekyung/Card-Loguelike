using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/SFX")]
public class SFXSO : ScriptableObject
{
    public string sfxName;
    public SfxType sfxType;
    public AudioClip audioClip;
}
