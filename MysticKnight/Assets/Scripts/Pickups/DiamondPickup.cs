using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondPickup : MonoBehaviour
{
    AudioSource pickupSound;

    // to be disabled on pickup
    SpriteRenderer spriteRenderer;
    CircleCollider2D hitbox;

    private void Start()
    {
        pickupSound = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        hitbox = GetComponent<CircleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D entity)
    {
        if (entity.tag == "Player")
        {
            entity.GetComponent<Player>().Pickup("diamond");
            pickupSound.Play();

            // make this object disappear
            spriteRenderer.enabled = false;
            hitbox.enabled = false;
        }
    }
}
