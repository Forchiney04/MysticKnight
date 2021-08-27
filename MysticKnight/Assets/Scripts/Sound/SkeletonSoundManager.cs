using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonSoundManager : MonoBehaviour
{
    static AudioSource audioSource;
    public static AudioClip enemyHit, enemyAttack;

    // Start is called before the first frame update
    void Start()
    {
        enemyHit = Resources.Load<AudioClip>("skeletonHit");
        enemyAttack = Resources.Load<AudioClip>("Attack");

        audioSource = GetComponent<AudioSource>();
    }

    public static void PlaySound(string sound)
    {
        switch (sound)
        {
            case "hit":
                audioSource.PlayOneShot(enemyHit);
                break;

            case "attack":
                audioSource.PlayOneShot(enemyAttack);
                break;

            default:
                Debug.Log("Sound not recognised");
                break;
        }
    }
}
