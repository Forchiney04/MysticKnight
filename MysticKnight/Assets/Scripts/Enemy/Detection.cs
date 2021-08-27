using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detection : MonoBehaviour
{

    Enemy enemyScript;

    void Start()
    {
        enemyScript = GetComponent<Enemy>();
    }

    void OnTriggerEnter2D(Collider2D entity)
    {
        if (entity.tag == "Player")
        {
            Debug.Log("Enter trigger");
            enemyScript.state = "aggro";
        }
    }

    void OnTriggerExit2D(Collider2D entity)
    {
        if (entity.tag == "Player")
        {
            Debug.Log("Exit trigger");
            if (enemyScript.startState == "patrol")
            {
                enemyScript.state = "patrol";
            }

            else
            {
                enemyScript.state = "paused";
                enemyScript.animator.SetTrigger("stopWalking");
            }
        }
    }
}
