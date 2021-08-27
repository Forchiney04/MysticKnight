using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmashBehaviour : StateMachineBehaviour
{
    Transform attackPos;
    public float attackRange;
    public LayerMask whatIsEnemies;
    GameObject boss;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss = GameObject.FindGameObjectWithTag("Boss");
        attackPos = GameObject.FindGameObjectWithTag("BossAttackPos").transform;
        BossSoundManager.PlaySound("attack");

        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
        for (int i = 0; i < enemiesToDamage.Length; i++)
        {
            enemiesToDamage[i].transform.GetComponent<Player>().TakeDamage(1, boss.transform);
        }
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
