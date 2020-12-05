using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public BoxCollider2D enemyFeet;
    private float cooldown = 2.0f;
    private bool isOnCooldown;

    private void Start()
    {
        enemyFeet.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CheckIfSteppedOn(collision.gameObject);
        if (collision.gameObject.layer == 10)
            enemyFeet.gameObject.SetActive(true);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!isOnCooldown)
        {
            StartCoroutine(Damaging(collision.gameObject));
        }
    }

    private void CheckIfSteppedOn(GameObject collideObject)
    {
        if (collideObject.GetComponent<PlayerController>())
        {
            Vector3 playerFeet = collideObject.GetComponent<PlayerController>().playerFeet.transform.position;
            if (playerFeet.y > transform.position.y)
            {
                Debug.Log($"Player's Feet Position : {playerFeet.y} is greater than Enemy's Center Position : {transform.position.y}");
                Destroy(gameObject);
            }
        }
    }

    IEnumerator Damaging(GameObject player)
    {
        isOnCooldown = true;
        player.GetComponent<Health>()?.TakeDamage();
        yield return new WaitForSeconds(cooldown);
        isOnCooldown = false;
    }
}
