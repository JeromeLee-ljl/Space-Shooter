using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource backgroundSource;

    public AudioSource gameSource;

    public AudioSource uiSource;

    public AudioClip gameBackgroundClip;

    public AudioClip startBackgroundClip;

    public static AudioManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    public void PlayGameBackground()
    {
        backgroundSource.clip = gameBackgroundClip;
        backgroundSource.Play();
    }

    public void StopGameBackground()
    {
        backgroundSource.Stop();
    }

    public void PlayStartBackground()
    {
        backgroundSource.clip = startBackgroundClip;
        backgroundSource.Play();
    }

    public void PlayGameClip(AudioClip clip)
    {
        gameSource.PlayOneShot(clip);
    }

    public void PlayUIClip(AudioClip clip)
    {
        uiSource.PlayOneShot(clip);
    }
}