using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Button audioButton;
    public Sprite soundOn;
    public Sprite soundOff;

    public void Pause()
    {
        Time.timeScale = (Time.timeScale == 1.0f) ? 0f : 1f;
    }

    public void ToggleAudio()
    {
        bool isOff = AudioManager.instance.Toggle();
        audioButton.image.sprite = (isOff) ? soundOff : soundOn;
    }
}
