using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundController : MonoBehaviour
{
    private AudioSource _audio;

    public static SoundController Instance { get; private set; }

    void Awake()
    {
        Instance = this;
        _audio = GetComponent<AudioSource>();
    }

    public void PlayEffect(string name)
    {
        AudioClip clip = Resources.Load<AudioClip>($"Sounds/{name}");
        _audio.PlayOneShot(clip);
    }
}
