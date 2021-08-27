using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeEnemy : MonoBehaviour
{
    public int health;

    // grounded
    public bool grounded = false;
    float groundCheckRadius = 0.05f;
    public LayerMask groundLayer;
    public Transform groundCheck;

    // knockback
    public float knockbackForce;

    // player
    GameObject player;

    public Animator animator;
    public Rigidbody2D rigidbody2d;
    public BoxCollider2D boxHitbox;
    public CircleCollider2D circleHitbox;
    public Transform attackPos;

    // camera shaking
    public float camShakeAmountOnHit;
    public float camShakeLengthOnHit;

    public GameObject playerCamera;
    CameraShake camShake;


    // Start is called before the first frame update
    void Start()
    {
        if (player == null) player = GameObject.FindGameObjectWithTag("Player");
        camShake = playerCamera.GetComponent<CameraShake>();
    }

    void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (grounded)
        {
            CheckIfDead();
        }
    }

    void CheckIfDead()
    {
        if (health <= 0)
        {
            Die();
        }
    }

    public void TakeDamage(int damage)
    {
        HandleKnockback(damage);
        animator.SetTrigger("hit");
        camShake.Shake(camShakeAmountOnHit, camShakeLengthOnHit);

        health -= damage;
    }

    public void HandleKnockback(int damage)
    {
        // if damage is lethal, do not apply knockback
        if (health - damage <= 0)
        {
            return;
        }

        if (transform.position.x < player.transform.position.x) // if hit from the right
        {
            rigidbody2d.AddForce(new Vector2(-knockbackForce, knockbackForce)); // add negative X force (knock us left)
        }

        else // if hit from the left
        {
            rigidbody2d.AddForce(new Vector2(knockbackForce, knockbackForce)); // add positive X force (knock us right)
        }
    }

    // Note: this function will disable this script on the enemy it is attached to
    public void Die()
    {
        animator.SetTrigger("die"); // play death animation
        rigidbody2d.velocity = new Vector2(0, 0);
        rigidbody2d.isKinematic = true; // disable physics

        // disable hitboxes
        boxHitbox.enabled = false;
        circleHitbox.enabled = false;

        // disable this script
        this.enabled = false;
        GetComponent<EnemyAttackMelee>().enabled = false; // disable enemy attacks
    }
}
