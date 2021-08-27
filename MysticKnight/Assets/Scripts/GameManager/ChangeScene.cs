using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    [SerializeField]
    private GameObject[] characters;
    private int characterIndex;


    public void Changecharacter(int index)
    {
        for (int i = 0; i < characters.Length; i++)
        {
            characters[i].SetActive(false);

        }
        characters[index].SetActive(true);
    }
    public void StartGame()
    {
        SceneManager.LoadScene(2);
        PlayerPrefs.SetInt("CharacterIndex", characterIndex);


    }
}
