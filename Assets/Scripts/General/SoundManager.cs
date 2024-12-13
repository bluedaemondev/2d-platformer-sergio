using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource sourceMusic;
    public AudioSource sourceAmbient;
    public AudioSource sourceSfx;

    public void PlayOnce(AudioClip clip)
    {
        sourceSfx.clip = clip;
        sourceSfx.Play();
    }
    public void PlayMusic()
    {
        sourceMusic.Play();
        sourceMusic.loop = true;
    }
    public void PlayAmbient()
    {
        sourceAmbient.Play();
        sourceAmbient.loop = true;
    }

}
