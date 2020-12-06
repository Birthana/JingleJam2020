using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{
    public int maxLives;
    private int currentLives;

    private void Start()
    {
        currentLives = maxLives;
        PlayerHealthUI.instance.SetNumberOfLives(currentLives);
        SetupHealth();
    }

    public void SetupHealth()
    {
        currentHealth = maxHealth;
        PlayerHealthUI.instance.CreateHearts(maxHealth);
    }

    public override void TakeDamage()
    {
        currentHealth--;
        GameManager.instance.HurtPlayer();
        PlayerHealthUI.instance.TakeDamage();
        AudioManager.instance.PlaySound(2);
        if (currentHealth <= 0)
        {
            currentLives--;
            PlayerHealthUI.instance.SetNumberOfLives(currentLives);
            GameManager.instance.Respawn();
            if (currentLives < 0)
            {
                GameManager.instance.GameOver();
            }
        }
    }
}
