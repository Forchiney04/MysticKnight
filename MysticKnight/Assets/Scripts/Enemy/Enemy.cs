using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public float speed;
    public string startState = "patrol";
    public string state = "patrol"; // default enemy state
    public float stoppingDistance;

    // grounded
    public bool grounded = false;
    float groundCheckRadius = 0.05f;
    public LayerMask groundLayer;
    public Transform groundCheck;

    // patrolling
    public float rayDistance;
    private bool movingRight = true;

    public Transform groundDetection;

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

            // check enemy state, act accordingly
            switch (state)
            {
                case "patrol":
                    Patrol();
                    break;

                case "aggro":
                    Follow(player.GetComponent<BoxCollider2D>());
                    break;

                default:
                    animator.SetTrigger("stopWalking");
                    break;
            }
        }
    }

    void Follow(Collider2D entity)
    {
        // return cases
        if (!grounded) return;
        if (Vector2.Distance(transform.position, entity.transform.position) <= stoppingDistance)
        {
            animator.SetTrigger("stopWalking");
            return;
        }

        if (entity.transform.position.x < transform.position.x) // if entity is to the left
        {
            if (movingRight) Flip();
            rigidbody2d.velocity = new Vector2(-speed, rigidbody2d.velocity.y); // move left
        }

        else // if entity is to the right
        {
            if (!movingRight) Flip();
            rigidbody2d.velocity = new Vector2(speed, rigidbody2d.velocity.y); // move right
        }
        animator.SetFloat("speed", speed); // play walk animation
    }

    void Patrol()
    {
        if (!grounded) return;

        if (movingRight)
        {
            rigidbody2d.velocity = new Vector2(speed, rigidbody2d.velocity.y); // move right
        }
        else
        {
            rigidbody2d.velocity = new Vector2(-speed, rigidbody2d.velocity.y); // move left
        }
        animator.SetFloat("speed", speed); // play walk animation

        // check if we're at an edge
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, rayDistance);
        if (groundInfo.collider == false) // if edge detected:
        {
            // change direction
            if (movingRight)
            {
                Flip();
                movingRight = false;
            }

            else
            {
                Flip();
                movingRight = true;
            }
        }
    }

    void Flip()
    {
        movingRight = !movingRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1; // make the scale opposite to what it was (flip graphic)
        transform.localScale = scale; // apply it

        // move ground detector to the other side of the character
        groundDetection.position.Set(
            groundDetection.position.x * -1,
            groundDetection.position.y,
            groundDetection.position.z);

        // move attack radius to the other side of the character
        attackPos.position.Set(
            attackPos.position.x * -1,
            attackPos.position.y,
            attackPos.position.z);
    }

    void CheckIfDead()
    {
        if (health <= 0 && state != "dead")
        {
            Die();
        }
    }

    public void TakeDamage(int damage)
    {
        HandleKnockback(damage);
        health -= damage;

        animator.SetTrigger("hit");
        SkeletonSoundManager.PlaySound("hit");
        camShake.Shake(camShakeAmountOnHit, camShakeLengthOnHit);
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
        state = "dead";

        animator.SetTrigger("die"); // play death animation
        rigidbody2d.velocity = new Vector2(0, 0);
        rigidbody2d.isKinematic = true; // disable physics

        // disable hitboxes
        boxHitbox.enabled = false;
        circleHitbox.enabled = false;

        // disable this script
        this.enabled = false;
        GetComponent<EnemyAttackMelee>().enabled = false; // disable enemy attacks
        // disable this script
        this.enabled = false;
        GetComponent<EnemyAttackMelee>().enabled = false; // disable enemy attacks
    }
}

