using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuManager : MonoBehaviour
{
    public void ButtonStart()
    {
        SceneManager.LoadScene("MainScene");
    }
    public void ButtonQuit()
    {
        Debug.Log(" quit the application");
        Application.Quit();
    }

}




