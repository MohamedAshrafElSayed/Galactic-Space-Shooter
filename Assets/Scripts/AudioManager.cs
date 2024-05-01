using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioData[] _audioData;

    void Start()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            foreach (AudioData _audio in _audioData)
            {
                AudioSource _audioSource = gameObject.AddComponent<AudioSource>();
                _audio.AudioSource = _audioSource;
                _audioSource.clip = _audio.Clip;
                _audioSource.volume = _audio.Volume;
                _audioSource.loop = _audio.IsLooping;
            }
        }
    }

    public void Play(string audio)
    {
        AudioData _audio = Array.Find(_audioData, item => item.AudioName == audio);
        if (_audio != null && _audio.AudioSource != null)
        {
            _audio.AudioSource.Play();
        }
    }

    public void Stop(string audio)
    {
        AudioData _audio = Array.Find(_audioData, item => item.AudioName == audio);
        if (_audio != null && _audio.AudioSource != null)
        {
            _audio.AudioSource.Stop();
        }
    }
}