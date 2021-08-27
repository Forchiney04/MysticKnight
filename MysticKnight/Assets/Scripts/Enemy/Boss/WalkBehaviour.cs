using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkBehaviour : StateMachineBehaviour
{
    public float timer;
    public float minTime;
    public float maxTime;

    private Transform player;
    private Transform boss;
    public float speed;
    private bool facingRight = true;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = Random.Range(minTime, maxTime);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        boss = GameObject.FindGameObjectWithTag("Boss").GetComponent<Transform>();
        Boss bossScript = boss.GetComponent<Boss>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (timer <= 0)
        {
            animator.SetTrigger("idle");
        }

        else
        {
            timer -= Time.deltaTime;
            if (player.transform.position.x < boss.position.x && facingRight == true)
            {
                Flip();
            }

            else if (player.transform.position.x > boss.position.x && facingRight == false)
            {
                Flip();
            }
            Vector2 target = new Vector2(player.position.x, boss.transform.position.y);
            boss.transform.position = Vector2.MoveTowards(boss.transform.position, target, speed * Time.deltaTime);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    void Flip()
    {
        facingRight = !facingRight;

        Vector3 scale = boss.transform.localScale;
        scale.x *= -1; // make the scale opposite to what it was (flip graphic)
        boss.transform.localScale = scale; // apply it
    }
}
