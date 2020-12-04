using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float cooldown = 2.0f;
    private bool isOnCooldown;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!isOnCooldown)
        {
            StartCoroutine(Damaging(collision.gameObject));
        }
    }

    IEnumerator Damaging(GameObject player)
    {
        isOnCooldown = true;
        player.GetComponent<Health>().TakeDamage();
        yield return new WaitForSeconds(cooldown);
        isOnCooldown = false;
    }
}
