using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartGame : MonoBehaviour
{
    public float restartTimeDeath;
    public float restartTimeCredits;
    bool restartNow = false;
    float resetTime;



    // Update is called once per frame
    void Update()
    {
        if (restartNow == true && resetTime <= Time.time)
        {
            Application.LoadLevel(Application.loadedLevel);
        }
    }

    public void Restart()
    {
        restartNow = true;
        resetTime = Time.time + restartTimeDeath;
    }

    public void PlayCredits()
    {

        restartNow = true;

    }
}
