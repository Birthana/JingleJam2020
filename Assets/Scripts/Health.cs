using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public virtual void TakeDamage()
    {
        currentHealth--;
        if (currentHealth <= 0)
        {
            AudioManager.instance.PlaySound(12);
            Destroy(gameObject);
        }
    }
}
