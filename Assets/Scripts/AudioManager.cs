using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioClip[] audios;
    public AudioSource backgroundPlayer;
    public AudioSource soundPlayer1;
    public AudioSource soundPlayer2;

    private bool isOff = false;

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
        if (audioClip == 4 || audioClip == 5)
        {
            if (soundPlayer2.isPlaying)
                return;
            soundPlayer2.clip = audios[audioClip];
            soundPlayer2.Play();
        }
        else
        {
            soundPlayer2.Stop();
            soundPlayer1.clip = audios[audioClip];
            soundPlayer1.Play();
        }
    }

    public bool Toggle()
    {
        if (isOff)
        {
            isOff = false;
            backgroundPlayer.enabled = true;
            soundPlayer1.enabled = true;
            soundPlayer2.enabled = true;
        }
        else
        {
            isOff = true;
            backgroundPlayer.enabled = false;
            soundPlayer1.enabled = false;
            soundPlayer2.enabled = false;
        }
        return isOff;
    }
}
