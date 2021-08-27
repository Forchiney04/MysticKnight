using System;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    Animator animator;
    BoxCollider2D boxHitbox;
    CircleCollider2D circleHitbox;

    public RestartGame gameManager;

    // stats
    bool alive = true;
    public int health;
    public int numOfHearts;
    public int damage;
    public float knockbackForce; // what force the player RECEIVES on knockback

    // items
    int diamonds = 0;

    // UI 
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public Text numDiamondsText;
    public Text gameOverText;
    Notification notifications;


    // movement
    public float maxSpeed;
    bool facingRight;
    float timeBtwFootstep;
    float startTimeBtwFootstep = 0.35f;


    // jumping
    bool grounded = false;
    float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float jumpHeight;
    // double/triple/etc jumping
    public int extraJumpsValue;
    public int extraJumps;

    // camera shaking
    public float camShakeAmountOnAttack;
    public float camShakeLengthOnAttack;

    public float camShakeAmountOnHit;
    public float camShakeLengthOnHit;

    public GameObject playerCamera;
    CameraShake camShake;

    PlayerAttack playerAttack;

    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxHitbox = GetComponent<BoxCollider2D>();
        circleHitbox = GetComponent<CircleCollider2D>();
        camShake = playerCamera.GetComponent<CameraShake>();
        notifications = GetComponent<Notification>();
        playerAttack = GetComponent<PlayerAttack>();
    }

    void Update()
    {
        // UI 
        HealthUI();
        DiamondsUI();

        // status checks
        CheckIfDead();

        // handle jump input
        if (grounded == true)
        {
            extraJumps = extraJumpsValue;
        }

        if (Input.GetKeyDown(KeyCode.W) && extraJumps > 0)
        {
            rigidbody2d.velocity = Vector2.up * jumpHeight;
            animator.SetBool("isGrounded", grounded);
            PlayerSoundManager.PlaySound("jump");
            extraJumps--;
        }
    }

    void FixedUpdate()
    {
        // check ground if not fall
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        animator.SetBool("isGrounded", grounded);

        animator.SetFloat("verticalSpeed", rigidbody2d.velocity.y);

        // move character on move command
        float move = Input.GetAxis("Horizontal");
        animator.SetFloat("speed", Mathf.Abs(move)); // Plays run animation

        // play footstep sound
        if (animator.GetFloat("speed") != 0 && grounded == true && timeBtwFootstep <= 0)
        {
            timeBtwFootstep = startTimeBtwFootstep;
            PlayerSoundManager.PlaySound("footstep");
        }
        else
        {
            timeBtwFootstep -= Time.deltaTime;
        }

        rigidbody2d.velocity = new Vector2(move * maxSpeed, rigidbody2d.velocity.y);

        // flip character if we move but are facing the wrong way
        if (move > 0 && facingRight)
        {
            Flip();
        }
        else if (move < 0 && !facingRight)
        {
            Flip();
        }
    }

    public void Pickup(String item)
    {
        switch (item)
        {
            case "diamond":
                if (diamonds == 24)
                {
                    playerAttack.damage = 5;
                    notifications.ShowNotification("25 diamonds: Weapon damage has  increased");
                }

                else if (diamonds == 39)
                {
                    extraJumpsValue = 2;
                    notifications.ShowNotification("Triple jump unlocked");
                }
                diamonds += 1;
                break;

            case "heart container":
                numOfHearts += 1;
                health += 10;
                break;

            case "health potion":
                health += 2;
                break;

            default:
                Debug.Log("Item not recognised on pickup");
                break;
        }
    }

    void HealthUI()
    {

        for (int i = 0; i < hearts.Length; i++)
        {
            if (health > numOfHearts)
            {
                health = numOfHearts;
            }

            // Show heart or empty heart based on health
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }

            else
            {
                hearts[i].sprite = emptyHeart;
            }

            // Display health cores
            if (i < numOfHearts)
            {
                hearts[i].enabled = true;
            }

            else
            {
                hearts[i].enabled = false;
            }
        }
    }

    void DiamondsUI()
    {
        numDiamondsText.text = diamonds.ToString();
    }

    // flip our character's image
    void Flip()
    {
        facingRight = !facingRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1; // make the scale opposite to what it was (flip graphic)
        transform.localScale = scale; // apply it
    }

    void CheckIfDead()
    {
        if (health <= 0 && alive == true)
        {
            Die();
        }
    }

    void Die()
    {
        alive = false;
        animator.SetTrigger("die");
        rigidbody2d.velocity = new Vector2(0, 0);
        rigidbody2d.isKinematic = true; // disable physics

        // disable hitboxes
        boxHitbox.enabled = false;
        circleHitbox.enabled = false;
        this.enabled = false;

        Animator gameOverAnimator = gameOverText.GetComponent<Animator>();
        gameOverAnimator.SetTrigger("GameOver");
        gameManager.Restart();
    }

    public void TakeDamage(int damage, Transform enemy)
    {
        HandleKnockback(damage, enemy);
        animator.SetTrigger("hit");
        PlayerSoundManager.PlaySound("hit");
        camShake.Shake(camShakeAmountOnHit, camShakeLengthOnHit);

        health -= damage;
    }

    public void TakeSpikeDamage(int damage, string type)
    {
        animator.SetTrigger("hit");
        PlayerSoundManager.PlaySound("hit");
        camShake.Shake(camShakeAmountOnHit, camShakeLengthOnHit);

        health -= damage;

        switch (type)
        {
            case "ground":
                rigidbody2d.velocity = Vector2.up * jumpHeight;
                break;

            case "ceiling":
                break;

            case "wall":
                break;

            default:
                Debug.Log("Wall type not identified");
                break;
        }
    }

    void HandleKnockback(int damage, Transform enemy)
    {
        // if damage is lethal, do not apply knockback
        if (health - damage <= 0)
        {
            return;
        }

        if (transform.position.x < enemy.transform.position.x) // if hit from the right
        {
            rigidbody2d.AddForce(new Vector2(-knockbackForce, knockbackForce)); // add negative X force (knock us left)
        }

        else // if hit from the left
        {
            rigidbody2d.AddForce(new Vector2(knockbackForce, knockbackForce)); // add positive X force (knock us right)
        }
    }

    public void WinGame()
    {
        gameManager.PlayCredits();
        playerAttack.enabled = false;
        this.enabled = false;
    }

}
