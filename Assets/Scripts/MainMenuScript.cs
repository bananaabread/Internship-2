using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    public GameObject Settings;
    //public GameObject settingsPanel;
    //public GameObject FrameRateText;
    public GameObject FrameRateOption;
    //public GameObject vSync;
    //public GameObject BackButton;

    public GameObject SoloButton;
    public GameObject VsButton;
    public GameObject SettingsButton;
    public GameObject QuitButton;
    public GameObject SoloButtonReal;
    public GameObject VsButtonReal;
    public GameObject SettingsButtonReal;
    public GameObject QuitButtonReal;

    private GameObject frameRateManager;
    public GameObject VSync;

    private void Start()
    {
        //if (VSync != null)
        //{
        //    if (PlayerPrefs.GetInt("VSyncOn", 1) == 0)
        //    {
        //        VSync.GetComponent<Toggle>().isOn = false;
        //    }
        //    if (PlayerPrefs.GetInt("VSyncOn", 1) == 1)
        //    {
        //        VSync.GetComponent<Toggle>().isOn = true;
        //    }
        //}
        frameRateManager = GameObject.FindGameObjectWithTag("FpsManager");
        StartCoroutine(removeMenu());
    }
    public IEnumerator removeMenu()
    {
        yield return new WaitForSeconds(0.01f);
        Settings.SetActive(false);
    }
    public void StartSoloMode()
    {
        SceneManager.LoadScene(1);
    }
    public void StartVsMode()
    {
        SceneManager.LoadScene(2);
    }
    public void OpenSettings()
    {
        Settings.SetActive(true);
        //settingsPanel.SetActive(true);
        //FrameRateText.SetActive(true);
        //FrameRateOption.SetActive(true);
        //vSync.SetActive(true);
        //BackButton.SetActive(true);

        SoloButton.GetComponent<ButtonScript>().enabled = false;
        VsButton.GetComponent<ButtonScript>().enabled = false;
        SettingsButton.GetComponent<ButtonScript>().enabled = false;
        QuitButton.GetComponent<ButtonScript>().enabled = false;
        SoloButtonReal.GetComponent<Button>().enabled = false;
        VsButtonReal.GetComponent<Button>().enabled = false;
        SettingsButtonReal.GetComponent<Button>().enabled = false;
        QuitButtonReal.GetComponent<Button>().enabled = false;
    }
    public void CloseSettings()
    {
        Settings.SetActive(false);
        //settingsPanel.SetActive(false);
        //FrameRateText.SetActive(false);
        //FrameRateOption.SetActive(false);
        //vSync.SetActive(false);
        //BackButton.SetActive(false);

        SoloButton.GetComponent<ButtonScript>().enabled = true;
        VsButton.GetComponent<ButtonScript>().enabled = true;
        SettingsButton.GetComponent<ButtonScript>().enabled = true;
        QuitButton.GetComponent<ButtonScript>().enabled = true;
        SoloButtonReal.GetComponent<Button>().enabled = true;
        VsButtonReal.GetComponent<Button>().enabled = true;
        SettingsButtonReal.GetComponent<Button>().enabled = true;
        QuitButtonReal.GetComponent<Button>().enabled = true;
    }
    public void changeFrameRate()
    {
        int index = FrameRateOption.GetComponent<TMP_Dropdown>().value;
        switch (index)
        {
            case 0: frameRateManager.GetComponent<LimitFps>().SetRate(30); break;
            case 1: frameRateManager.GetComponent<LimitFps>().SetRate(60); break;
            case 2: frameRateManager.GetComponent<LimitFps>().SetRate(90); break;
            case 3: frameRateManager.GetComponent<LimitFps>().SetRate(120); break;
            case 4: frameRateManager.GetComponent<LimitFps>().SetRate(240); break;
        }
    }
    public void VsyncToggle()
    {
        FrameRateOption.GetComponent<TMP_Dropdown>().interactable = !FrameRateOption.GetComponent<TMP_Dropdown>().interactable;
        frameRateManager.GetComponent<LimitFps>().ToggleVSync(VSync.GetComponent<Toggle>().isOn);
    }
    public void Exit()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
}
