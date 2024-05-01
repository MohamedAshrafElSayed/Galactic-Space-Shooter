using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameAudio/AudioData")]
public class AudioData : ScriptableObject
{
    [SerializeField] private string _audioName;
    [SerializeField] private AudioClip _clip;
    [Range(0f, 1f)]
    [SerializeField] private float _volume;
    [SerializeField] private bool _isLooping;
    private AudioSource _audioSource;

    public string AudioName => _audioName;
    public AudioClip Clip => _clip;
    public float Volume => _volume;
    public bool IsLooping => _isLooping;

    public AudioSource AudioSource { get => _audioSource; set => _audioSource = value; }
}
