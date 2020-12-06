using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float cooldown;
    private bool isOnCooldown = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() && !isOnCooldown)
        {
            StartCoroutine(Damaging(collision.gameObject));
        }
    }

    IEnumerator Damaging(GameObject player)
    {
        isOnCooldown = true;
        player.gameObject.GetComponent<PlayerHealth>().TakeDamage();
        yield return new WaitForSeconds(cooldown);
        isOnCooldown = false;
    }
}
