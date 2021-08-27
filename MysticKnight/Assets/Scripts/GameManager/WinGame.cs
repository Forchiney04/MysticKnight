using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinGame : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player playerWin = other.gameObject.GetComponent<Player>();
            playerWin.WinGame();
        }
    }
}
