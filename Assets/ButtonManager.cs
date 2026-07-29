using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public GameObject PlayButton;
    public GameObject VsButton;
    public GameObject SettingsButton;
    public GameObject HighScoresButton;
    public GameObject QuitButton;

    // Start is called before the first frame update
    void Start()
    {
        PlayButton.GetComponent<ButtonScript>().canEnter = true;
        VsButton.GetComponent<ButtonScript>().canEnter = true;
        SettingsButton.GetComponent<ButtonScript>().canEnter = true;
        HighScoresButton.GetComponent<ButtonScript>().canEnter = true;
        QuitButton.GetComponent<ButtonScript>().canEnter = true;
    }
}
