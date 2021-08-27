using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpike : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Player>().TakeSpikeDamage(1, "ground");
        }

        else if (collision.tag == "Enemy")
        {
            Enemy enemyScript = collision.GetComponent<Enemy>();
            if (enemyScript.state != "dead")
            {
                enemyScript.Die();
            }
        }
    }
}
