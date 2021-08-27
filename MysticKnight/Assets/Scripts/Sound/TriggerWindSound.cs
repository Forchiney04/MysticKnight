using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerWindSound : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (AmbientSoundManager.audioSource.clip.name != "wind")
            {
                AmbientSoundManager.PlaySound("wind");
            }
        }
    }
}
