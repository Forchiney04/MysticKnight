using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rigidbody2d;
    BoxCollider2D boxHitbox;
    public int health;
    public bool facingRight = true;
    private bool alive = true;

    // camera shaking
    public float camShakeAmountOnHit;
    public float camShakeLengthOnHit;
    public GameObject playerCamera;
    CameraShake camShake;

    public Transform attackPos;
    public float attackRange;
    public LayerMask whatIsEnemies;

    public GameObject bossBoundaries;
    public GameObject healthbar;
    public Slider healthBarSlider;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        boxHitbox = GetComponent<BoxCollider2D>();
        camShake = playerCamera.GetComponent<CameraShake>();
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        CheckIfDead();
        healthBarSlider.value = health;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && alive == true)
        {
            bossBoundaries.SetActive(true);
            healthbar.SetActive(true);
            animator.SetTrigger("idle");
            BossSoundManager.PlaySound("growl");
        }
    }

    public void TakeDamage(int damage)
    {
        if (alive)
        {
            health -= damage;
            BossSoundManager.PlaySound("hit");
            camShake.Shake(camShakeAmountOnHit, camShakeLengthOnHit);
        }
    }

    void CheckIfDead()
    {
        if (health <= 0 && alive == true)
        {
            Die();
        }
    }

    public void Die()
    {
        alive = false;

        BossSoundManager.PlaySound("death");
        bossBoundaries.SetActive(false);
        healthbar.SetActive(false);
        animator.SetTrigger("die"); // play death animation
        rigidbody2d.velocity = new Vector2(0, 0);
        rigidbody2d.isKinematic = true; // disable physics

        // disable hitboxes
        boxHitbox.enabled = false;

        // disable this script
        this.enabled = false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
