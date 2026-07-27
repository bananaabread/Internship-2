using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public GameObject PlayButton;
    public GameObject VsButton;
    public GameObject SettingsButton;
    public GameObject QuitButton;

    // Start is called before the first frame update
    void Start()
    {
        PlayButton.SetActive(true);
        VsButton.SetActive(true);
        SettingsButton.SetActive(true);
        QuitButton.SetActive(true);
    }
}
