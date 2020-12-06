using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public BoxCollider2D enemyFeet;
    public bool isRoaming;
    public float speed;
    public float distance = 2f;
    private bool movingRight = false;
    public Transform groundDetection;

    private float cooldown = 1.0f;
    private bool isOnCooldown;

    private void Start()
    {
        enemyFeet.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (isRoaming)
        {
            MoveBetweenStartEnd();
        }
    }

    private void MoveBetweenStartEnd()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance);
        if (groundInfo.collider == false)
        {
            if (movingRight == true)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
                movingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //CheckIfSteppedOn(collision.gameObject);
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
                Destroy(gameObject);
            }
        }
    }

    IEnumerator Damaging(GameObject player)
    {
        if (player.GetComponent<PlayerController>())
        {
            isOnCooldown = true;
            player.GetComponent<PlayerHealth>().TakeDamage();
            yield return new WaitForSeconds(cooldown);
            isOnCooldown = false;
        }
    }


}
