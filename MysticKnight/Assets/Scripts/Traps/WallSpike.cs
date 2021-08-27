using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSpike : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Player>().TakeSpikeDamage(1, "wall");
        }
    }
}
