using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSoundManager : MonoBehaviour
{
    static AudioSource audioSource;
    public static AudioClip attack, hit1, hit2, hit3, hit4, hit5, hit6, growl, death;

    // Start is called before the first frame update
    void Start()
    {
        attack = Resources.Load<AudioClip>("bossAttack");
        hit1 = Resources.Load<AudioClip>("shade1");
        hit2 = Resources.Load<AudioClip>("shade2");
        hit3 = Resources.Load<AudioClip>("shade3");
        hit4 = Resources.Load<AudioClip>("shade4");
        hit5 = Resources.Load<AudioClip>("shade5");
        hit6 = Resources.Load<AudioClip>("shade6");
        growl = Resources.Load<AudioClip>("bossGrowl");
        death = Resources.Load<AudioClip>("bossDeath");

        audioSource = GetComponent<AudioSource>();
    }

    public static void PlaySound(string sound)
    {
        switch (sound)
        {
            case "hit":
                int rand = Random.Range(0, 6);
                switch (rand)
                {
                    case 0:
                        audioSource.PlayOneShot(hit1);
                        break;

                    case 1:
                        audioSource.PlayOneShot(hit2);
                        break;

                    case 2:
                        audioSource.PlayOneShot(hit3);
                        break;

                    case 3:
                        audioSource.PlayOneShot(hit4);
                        break;

                    case 4:
                        audioSource.PlayOneShot(hit5);
                        break;

                    case 5:
                        audioSource.PlayOneShot(hit6);
                        break;

                    default:
                        Debug.Log("Boss hit not recognised");
                        break;
                }
                break;

            case "attack":
                audioSource.PlayOneShot(attack);
                break;

            case "growl":
                audioSource.PlayOneShot(growl);
                break;

            case "death":
                audioSource.PlayOneShot(death);
                break;

            default:
                Debug.Log("Sound not recognised");
                break;
        }
    }
}
