using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{
    private void Start()
    {
        currentHealth = maxHealth;
        PlayerHealthUI.instance.CreateHearts(maxHealth);
    }

    public override void TakeDamage()
    {
        base.TakeDamage();
        PlayerHealthUI.instance.TakeDamage();
        AudioManager.instance.PlaySound(2);
    }
}
