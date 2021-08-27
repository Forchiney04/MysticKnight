using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDungeonSound : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (AmbientSoundManager.audioSource.clip.name != "atmosphere-cave-loop")
            {
                AmbientSoundManager.PlaySound("dungeon");
            }
        }
    }
}
