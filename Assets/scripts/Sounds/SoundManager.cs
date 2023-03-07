using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    private static SoundManager instance;
    public static SoundManager Instance { get { return instance; } }

    public AudioSource soundEffect;
    public AudioSource soundMusic;
    public bool IsMute = false;
    public float Volume = 1;

    [SerializeField] 
    public Sound[] SoundType;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        SetVolume(0.5f);
        PlayMusic(global::SoundType.music);
    }
    public void PlayMusic(SoundType sound)
    {
        if (IsMute)
            return;

        AudioClip clip = getSoundClip(sound);
        if (clip != null)
        {
            soundMusic.clip = clip;
            soundMusic.Play();
        }
        else
        {
            Debug.LogError("error clip not found");
        }
    }
    public void Mute(bool status)
    {
        IsMute = status;
    }
    public void SetVolume(float volume)
    {
        Volume = volume;
        soundEffect.volume = Volume;
        soundMusic.volume = Volume;
    }
    public void Play(SoundType sound)
    {
        if (IsMute)
            return;

        AudioClip clip = getSoundClip(sound);
        if (clip != null)
        {
            soundEffect.PlayOneShot(clip);
        }
        else
        {
            Debug.LogError("error clip not found");
        }
    }

    private AudioClip getSoundClip(SoundType sound)
    {
        Sound item = Array.Find(SoundType, i => i.SoundType == sound);
        if (item != null)
        {
            return item.soundClip;
        }
        return null;
    }
}

[System.Serializable]
public class Sound
{
    public SoundType SoundType;
    public AudioClip soundClip;
}

public enum SoundType
{
    ButtonClick,
    Footstep,
    music,
    PlayerDeath,
    EnemyDeath,
    Jump,
   
    Key,
    FinishLevel,
}
