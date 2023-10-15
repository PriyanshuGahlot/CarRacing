using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class sound
{
    public string name;
    public AudioClip clip;
    [Range(0,1)]
    public float volume;
    public bool playOnAwake;
    public bool loop;
    [HideInInspector]
    public AudioSource audioSource;
}
