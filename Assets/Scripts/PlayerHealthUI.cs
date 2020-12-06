using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    public static PlayerHealthUI instance;
    public GameObject healthBar;
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

    public void CreateHearts(int currentHealth)
    {
        for (int i = 0; i < currentHealth / 2; i++)
        {
            Image newHeart = Instantiate(fullHeart, healthBar.transform);
            fullHeartsHealth.Add(newHeart);
        }
        if (currentHealth % 2 != 0)
        {
            halfHeartHealth = Instantiate(halfHeart, healthBar.transform);
        }
    }

    public void TakeDamage()
    {
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
    }
}
