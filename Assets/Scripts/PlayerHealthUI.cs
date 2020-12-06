using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealthUI : MonoBehaviour
{
    public static PlayerHealthUI instance;
    public GameObject healthBar;
    public TextMeshProUGUI playerLivesText;
    public Image fullHeart;
    public Image halfHeart;

    private List<Image> fullHeartsHealth = new List<Image>();
    private Image halfHeartHealth;

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

    public void SetNumberOfLives(int lives)
    {
        playerLivesText.text = "" + lives;
    }

    public void CreateHearts(int currentHealth)
    {
        /**
        for (int i = 0; i < currentHealth / 2; i++)
        {
            Image newHeart = Instantiate(fullHeart, healthBar.transform);
            fullHeartsHealth.Add(newHeart);
        }
        if (currentHealth % 2 != 0)
        {
            halfHeartHealth = Instantiate(halfHeart, healthBar.transform);
        }
    */
        for (int i = 0; i < currentHealth; i++)
        {
            Image newHeart = Instantiate(fullHeart, healthBar.transform);
            fullHeartsHealth.Add(newHeart);
        }
    }

    public void TakeDamage()
    {
        /**
        if (halfHeartHealth == null)
        {
            Image lostHeart = fullHeartsHealth[0];
            fullHeartsHealth.RemoveAt(0);
            Destroy(lostHeart.gameObject);
            halfHeartHealth = Instantiate(halfHeart, healthBar.transform);
        }
        else
        {
            Destroy(halfHeartHealth.gameObject);
        }
    */
        Image lostHeart = fullHeartsHealth[0];
        fullHeartsHealth.RemoveAt(0);
        Destroy(lostHeart.gameObject);
    }
}
