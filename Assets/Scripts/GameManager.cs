using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public PlayerController player;
    public Button audioButton;
    public Sprite soundOn;
    public Sprite soundOff;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        AudioManager.instance.PlayBackGround(6);
    }

    public void Pause()
    {
        Time.timeScale = (Time.timeScale == 1.0f) ? 0f : 1f;
    }

    public void ToggleAudio()
    {
        bool isOff = AudioManager.instance.Toggle();
        audioButton.image.sprite = (isOff) ? soundOff : soundOn;
    }

    public void Respawn()
    {
        player.transform.position = transform.position;
        player.GetComponent<PlayerHealth>().SetupHealth();
    }

    public void GameOver()
    {
        SceneManager.LoadScene(2);
    }

    public void StopTalking()
    {
        player.GetComponent<PlayerController>().StopTalking();
    }

    public void HurtPlayer()
    {
        player.GetComponent<PlayerController>().PlayHurtAnimation();
    }
}
