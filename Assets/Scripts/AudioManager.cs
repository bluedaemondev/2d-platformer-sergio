using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSourceMusic;
    public AudioSource audioSourceAmbient;
    public AudioSource audioSourceSfx;
    public void PlayMusic()
    {
        audioSourceMusic.loop = true;
        audioSourceMusic.Play();
    }
    public void PlayMusic(AudioClip music)
    {
        audioSourceMusic.loop = true;
        audioSourceMusic.clip = music;
        audioSourceMusic.Play();
    }
    public void PlayAmbient(AudioClip clip)
    {
        audioSourceAmbient.loop = true;
        audioSourceAmbient.Play();
    }

    public void PlayOnce(AudioClip clip)
    {
        audioSourceSfx.loop = false;
        audioSourceSfx.clip = clip;
        audioSourceSfx.Play();
    }

}
