using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioClip[] audios;
    public AudioSource backgroundPlayer;
    public AudioSource soundPlayer;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayBackGround(int audioClip)
    {
        backgroundPlayer.clip = audios[audioClip];
        backgroundPlayer.Play();
    }

    public void PlaySound(int audioClip)
    {
        soundPlayer.clip = audios[audioClip];
        soundPlayer.Play();
    }
}
