using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;

    public AudioClip clip;
    public AudioMixer audioMixer;

    public float minPitch;
    public float maxPitch;

    [Range(0f, 1f)]
    public float volume;
    [Range(-3f, 3f)]
    public float pitch;
    public string outputMixer;
   
    //3D Audio
    /*
    public float spatialBlend;
    public AudioRolloffMode rolloffMode;
    public float dopplerLevel;
    public float maxDistance;
    */ 
    public bool loop;
    public bool PlayOnAwake;

    [HideInInspector]
    public AudioSource source;

}
