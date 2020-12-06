using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public LayerMask playerLayer;
    public BoxCollider2D enemyFeet;
    public GameObject weaponHitBox;
    public bool isRoaming;
    public bool hasProjectiles;
    public GameObject projectilePrefab;
    public float speed;
    public float distance = 2f;
    public float attackRange = 4.0f;
    private bool movingRight = false;
    public Transform groundDetection;

    private float cooldown = 1.0f;
    private bool isOnCooldown;
    private bool isAttacking = false;
    private Animator anim;

    private void Start()
    {
        enemyFeet.gameObject.SetActive(false);
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isRoaming && !isAttacking)
        {
            anim.SetBool("isRoaming", true);
            MoveBetweenStartEnd();
            CheckAttack();
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

    private void CheckAttack()
    {
        if (isNextToPlayer() && !isAttacking)
        {
            StartCoroutine(Attacking());
        }
    }

    IEnumerator Attacking()
    {
        anim.SetBool("isAttacking", true);
        anim.SetBool("isRoaming", false);
        if (hasProjectiles)
        {
            isAttacking = true;
            GameObject newProjectile = Instantiate(projectilePrefab, transform);
            newProjectile.transform.position = transform.position;
            newProjectile.GetComponent<Projectile>().isLeft = (transform.rotation.y == 0) ? false : true;
            Destroy(newProjectile, 2.0f);
            yield return new WaitForSeconds(1.5f);
            isAttacking = false;
        }
        else
        {
            AudioManager.instance.PlaySound(13);
            isAttacking = true;
            weaponHitBox.SetActive(true);
            yield return new WaitForSeconds(1.5f);
            weaponHitBox.SetActive(false);
            isAttacking = false;
        }
        anim.SetBool("isRoaming", true);
        anim.SetBool("isAttacking", false);
    }

    private bool isNextToPlayer()
    {
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        RaycastHit2D raycastHit = Physics2D.Raycast(collider.bounds.center, (transform.rotation.y == 0f) ? Vector2.right : Vector2.left, collider.bounds.extents.x + attackRange, playerLayer);
        return raycastHit.collider != null;
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
            //StartCoroutine(Damaging(collision.gameObject));
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
