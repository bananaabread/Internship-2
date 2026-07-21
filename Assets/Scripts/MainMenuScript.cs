using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public void StartSoloMode()
    {
        SceneManager.LoadScene(1);
    }
    public void StartVsMode()
    {
        SceneManager.LoadScene(2);
    }
    public void Exit()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
}
