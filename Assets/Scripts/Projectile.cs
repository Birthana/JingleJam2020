using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public bool isLeft = false;
    private Rigidbody2D rb;

    private void Start()
    {
        AudioManager.instance.PlaySound(11);
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2((isLeft) ? -1 : 1 * speed, rb.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage();
            Destroy(gameObject);
        }
    }
}
