using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("");
    }

    public void TestZone()
    {
        SceneManager.LoadScene("TestZone");
    }

    public void OpenSettings()
    {
        SceneManager.LoadScene("");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    
}
