using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public LayerMask groundLayer;
    public LayerMask npcLayer;
    public float speed;
    public float jumpForce;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2.0f;
    public bool isJumping = false;
    public bool isAttacking = false;
    public bool isTalking = false;
    private bool isConversing = false;

    public GameObject playerFeet;
    public GameObject weaponHitbox;

    private Rigidbody2D rb;
    private Animator anim;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckTalk();
        if (isTalking)
            return;
        float h = Input.GetAxis("Horizontal");
        if (h == 0)
        {
            anim.SetBool("isRunning", false);
        }
        else
        {
            anim.SetBool("isRunning", true);
            if(!isJumping)
                AudioManager.instance.PlaySound(Random.Range(0,2) + 4);
            if(h > 0)
            {
                transform.rotation = new Quaternion(0, 0, 0, 0);
            }
            else
            {
                transform.rotation = new Quaternion(0, 180, 0, 0);
            }
        }
        rb.velocity = new Vector2(h * speed, rb.velocity.y);
        CheckIfOutOfBounds();
        CheckJump();
        CheckAttack();
        ApplyMultipliers();
    }

    private void CheckIfOutOfBounds()
    {
        if (transform.position.y < -7.0f)
        {
            GetComponent<PlayerHealth>().TakeDamage();
        }
    }

    private void CheckJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping && isGrounded())
        {
            isJumping = true;
            rb.velocity = Vector2.up * jumpForce;
            AudioManager.instance.PlaySound(3);
            anim.SetBool("isJumping", true);
        }
        else
        {
            anim.SetBool("isJumping", false);
        }
    }

    private bool isGrounded()
    {
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        RaycastHit2D raycastHit = Physics2D.BoxCast(collider.bounds.center, collider.bounds.size, 0f, Vector2.down, 1f, groundLayer);
        return raycastHit.collider != null;
    }

    private bool isNextToNPC()
    {
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        RaycastHit2D raycastHit = Physics2D.Raycast(collider.bounds.center, Vector2.right, collider.bounds.extents.x + 0.01f, npcLayer);
        return raycastHit.collider != null;
    }

    private void CheckAttack()
    {
        if (Input.GetKeyDown(KeyCode.Z) && !isAttacking)
        {
            StartCoroutine(Attacking());
        }
    }

    IEnumerator Attacking()
    {
        isAttacking = true;
        weaponHitbox.SetActive(true);
        anim.SetBool("isAttacking", true);
        AudioManager.instance.PlaySound(1);
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("isAttacking", false);
        weaponHitbox.SetActive(false);
        isAttacking = false;
    }

    public void CheckTalk()
    {
        if (Input.GetKeyDown(KeyCode.X) && !isTalking && isNextToNPC())
        {
            StartCoroutine(Talking());
        }
        else if(Input.GetKeyDown(KeyCode.X) && !isConversing && isNextToNPC())
        {
            StartCoroutine(Conversing());
        }
    }

    IEnumerator Talking()
    {
        isTalking = true;
        rb.velocity = new Vector2(0, rb.velocity.y);
        transform.rotation = new Quaternion(0, 0, 0, 0);
        anim.SetBool("isTalking", true);
        yield return new WaitForSeconds(0.1f);
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        RaycastHit2D raycastHit = Physics2D.Raycast(collider.bounds.center, Vector2.right, collider.bounds.extents.x + 0.01f, npcLayer);
        TextMeshPro npcDialogueBox = raycastHit.collider.gameObject.GetComponent<NPC>().dialogueBox;
        DialogueManager.instance.DisplayDialogue(npcDialogueBox);
    }

    IEnumerator Conversing()
    {
        isConversing = true;
        DialogueManager.instance.DisplayNext();
        yield return new WaitForSeconds(0.1f);
        isConversing = false;
    }

    public void StopTalking()
    {
        isTalking = false;
        anim.SetBool("isTalking", false);
    }

    public void PlayHurtAnimation()
    {
        StartCoroutine(Hurting());
    }

    IEnumerator Hurting()
    {
        anim.SetBool("isHurt", true);
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("isHurt", false);
    }

    private void ApplyMultipliers()
    {
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector2.up * Physics2D.gravity * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }
}
