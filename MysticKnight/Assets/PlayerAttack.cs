using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private float timeBtwAttack;
    public float startTimeBtwAttack;
    Animator animator;
    public Transform attackPos;
    public float attackRange;
    public LayerMask whatIsEnemies;
    public int damage;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        CheckIfAttack();
    }

    void CheckIfAttack()
    {
        // check time between attack
        if (timeBtwAttack <= 0)
        {
            // then you can attack
            timeBtwAttack = startTimeBtwAttack;
            if (Input.GetKey(KeyCode.Space)) // space to attack
            {
                animator.SetTrigger("attack");
                PlayerSoundManager.PlaySound("attack");

                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    if (enemiesToDamage[i].transform.parent.name == "AiEnemySlimeStatic")
                    {
                        enemiesToDamage[i].transform.parent.GetComponent<SlimeEnemy>().TakeDamage(damage);
                    }

                    else if (enemiesToDamage[i].transform.parent.name == "Boss")
                    {
                        enemiesToDamage[i].transform.parent.GetComponent<Boss>().TakeDamage(damage);
                    }

                    else
                    {
                        enemiesToDamage[i].transform.parent.GetComponent<Enemy>().TakeDamage(damage);
                    }
                }
            }

        }

        else
        {
            timeBtwAttack -= Time.deltaTime;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}