using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientSoundManager : MonoBehaviour
{
    public static AudioSource audioSource;
    public static AudioClip dungeonAmbient, windAmbient;

    // Start is called before the first frame update
    void Start()
    {
        dungeonAmbient = Resources.Load<AudioClip>("atmosphere_cave_loop");
        windAmbient = Resources.Load<AudioClip>("wind");

        audioSource = GetComponent<AudioSource>();
    }

    public static void PlaySound(string sound)
    {
        switch (sound)
        {
            case "dungeon":
                audioSource.clip = dungeonAmbient;
                audioSource.Play();
                break;

            case "wind":
                audioSource.clip = windAmbient;
                audioSource.Play();
                break;

            default:
                Debug.Log("Sound not recognised");
                break;
        }
    }
}
