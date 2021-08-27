using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundManager : MonoBehaviour
{
    static AudioSource audioSource;
    public static AudioClip playerHit1, playerHit2, playerHit3, playerHit4, playerAttack, playerJump,
        footstep1, footstep2, footstep3, footstep4, footstep5, footstep6, footstep7;

    // Start is called before the first frame update
    void Start()
    {
        playerHit1 = Resources.Load<AudioClip>("maleHurt1");
        playerHit2 = Resources.Load<AudioClip>("maleHurt2");
        playerHit3 = Resources.Load<AudioClip>("maleHurt3");
        playerHit4 = Resources.Load<AudioClip>("maleHurt4");
        playerAttack = Resources.Load<AudioClip>("Attack");
        playerJump = Resources.Load<AudioClip>("Jump 2");

        footstep1 = Resources.Load<AudioClip>("footstep-dirt-01");
        footstep2 = Resources.Load<AudioClip>("footstep-dirt-02");
        footstep3 = Resources.Load<AudioClip>("footstep-dirt-03");
        footstep4 = Resources.Load<AudioClip>("footstep-dirt-04");
        footstep5 = Resources.Load<AudioClip>("footstep-dirt-05");
        footstep6 = Resources.Load<AudioClip>("footstep-dirt-06");
        footstep7 = Resources.Load<AudioClip>("footstep-dirt-07");

        audioSource = GetComponent<AudioSource>();
    }

    public static void PlaySound(string sound)
    {
        switch (sound)
        {
            case "hit":
                int rand = Random.Range(1, 4);
                if (rand == 1) audioSource.PlayOneShot(playerHit1);
                else if (rand == 2) audioSource.PlayOneShot(playerHit2);
                else if (rand == 3) audioSource.PlayOneShot(playerHit3);
                else if (rand == 4) audioSource.PlayOneShot(playerHit4);
                break;

            case "attack":
                audioSource.PlayOneShot(playerAttack);
                break;

            case "jump":
                audioSource.PlayOneShot(playerJump);
                break;

            case "footstep":
                int footstep = Random.Range(0, 7);
                switch (footstep)
                {
                    case 0:
                        audioSource.PlayOneShot(footstep1);
                        break;

                    case 1:
                        audioSource.PlayOneShot(footstep2);
                        break;

                    case 2:
                        audioSource.PlayOneShot(footstep3);
                        break;

                    case 3:
                        audioSource.PlayOneShot(footstep4);
                        break;

                    case 4:
                        audioSource.PlayOneShot(footstep5);
                        break;

                    case 5:
                        audioSource.PlayOneShot(footstep6);
                        break;

                    case 6:
                        audioSource.PlayOneShot(footstep7);
                        break;

                    default:
                        Debug.Log("Footstep not recognised");
                        break;
                }
                break;

            default:
                Debug.Log("Sound not recognised");
                break;
        }
    }
}
