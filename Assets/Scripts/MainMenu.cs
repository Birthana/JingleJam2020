using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public GameObject playButton;
    public GameObject creditsButton;
    public GameObject quitButton;
    public GameObject backButton;
    public TextMeshProUGUI creditsText;

    private void Start()
    {
        /**
        playButton.SetActive(true);
        creditsButton.SetActive(true);
        quitButton.SetActive(true);
        backButton.SetActive(false);
        creditsText.gameObject.SetActive(false);
        */
    }

    private void Update()
    {
        if (Input.anyKey)
        {
            Debug.Log("Pressed.");
            Play();
        }
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void Credits()
    {
        playButton.SetActive(false);
        creditsButton.SetActive(false);
        quitButton.SetActive(false);
        backButton.SetActive(true);
        creditsText.gameObject.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quitting Application.");
    }

    public void Back()
    {
        playButton.SetActive(true);
        creditsButton.SetActive(true);
        quitButton.SetActive(true);
        backButton.SetActive(false);
        creditsText.gameObject.SetActive(false);
    }
}
