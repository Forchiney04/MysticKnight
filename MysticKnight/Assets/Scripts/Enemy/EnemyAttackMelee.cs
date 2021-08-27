using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackMelee : MonoBehaviour
{
    // attacking
    // sets a limit to our attack speed
    private float timeBtwAttack;
    public float startTimeBtwAttack;

    public Transform attackPos;
    public float attackRange;
    public LayerMask whatIsEnemies;
    public int damage;

    public Animator animator;

    // Update is called once per frame
    void Update()
    {
        Attack();
    }

    void Attack()
    {
        // check time between attack
        if (timeBtwAttack <= 0)
        {
            // then you can attack
            timeBtwAttack = startTimeBtwAttack;


            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                animator.SetTrigger("attack");
                SkeletonSoundManager.PlaySound("attack");
                enemiesToDamage[i].transform.GetComponent<Player>().TakeDamage(damage, this.transform);
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
