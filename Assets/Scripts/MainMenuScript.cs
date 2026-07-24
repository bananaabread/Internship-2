using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
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

    [SerializeField] private AudioMixer _audioMixer;
    private float savedMasterValue;
    private float savedMusicValue;
    private float savedSFXValue;

    //public string typeCheck;

    public Slider MasterSlider;
    public Slider MusicSlider;
    public Slider SFXSlider;

    private void Start()
    {
        frameRateManager = GameObject.FindGameObjectWithTag("FpsManager");
        //StartCoroutine(removeMenu());
        Settings.SetActive(false);
        switch (PlayerPrefs.GetInt("FrameRate", 60))
        {
            case 30:
                FrameRateOption.GetComponent<TMP_Dropdown>().value = 0;
                break;
            case 60:
                FrameRateOption.GetComponent<TMP_Dropdown>().value = 1;
                break;
            case 90:
                FrameRateOption.GetComponent<TMP_Dropdown>().value = 2;
                break;
            case 120:
                FrameRateOption.GetComponent<TMP_Dropdown>().value = 3;
                break;
            case 240:
                FrameRateOption.GetComponent<TMP_Dropdown>().value = 4;
                break;
            case -1:
                FrameRateOption.GetComponent<TMP_Dropdown>().value = 5;
                break;
        }

        savedMasterValue = PlayerPrefs.GetFloat("SavedMasterVolume", 1);
        MasterSlider.value = PlayerPrefs.GetFloat("SavedMasterVolume", 1);
        _audioMixer.SetFloat("MasterVolume", Mathf.Log10(savedMasterValue) * 20);

        savedMusicValue = PlayerPrefs.GetFloat("SavedMusicVolume", 1);
        MusicSlider.value = PlayerPrefs.GetFloat("SavedMusicVolume", 1);
        _audioMixer.SetFloat("MusicVolume", Mathf.Log10(savedMusicValue) * 20);

        savedSFXValue = PlayerPrefs.GetFloat("SavedSFXVolume", 1);
        SFXSlider.value = PlayerPrefs.GetFloat("SavedSFXVolume", 1);
        _audioMixer.SetFloat("SFXVolume", Mathf.Log10(savedSFXValue) * 20);
    }
    public void SetMasterVolume()
    {
        _audioMixer.SetFloat("MasterVolume", Mathf.Log10(MasterSlider.value) * 20);
        PlayerPrefs.SetFloat("SavedMasterVolume", MasterSlider.value);
    }
    public void SetMusicVolume()
    {
        _audioMixer.SetFloat("MusicVolume", Mathf.Log10(MusicSlider.value) * 20);
        PlayerPrefs.SetFloat("SavedMusicVolume", MusicSlider.value);
    }
    public void SetSFXVolume()
    {
        _audioMixer.SetFloat("SFXVolume", Mathf.Log10(SFXSlider.value) * 20);
        PlayerPrefs.SetFloat("SavedSFXVolume", SFXSlider.value);
    }
    private void Update()
    {
        if (VSync.GetComponent<Toggle>().isOn)
        {
            FrameRateOption.GetComponent<TMP_Dropdown>().interactable = false;
        }
        if (!VSync.GetComponent<Toggle>().isOn)
        {
            FrameRateOption.GetComponent<TMP_Dropdown>().interactable = true;
        }
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
            case 0: frameRateManager.GetComponent<LimitFps>().SetRate(30); PlayerPrefs.SetInt("FrameRate", 30); break;
            case 1: frameRateManager.GetComponent<LimitFps>().SetRate(60); PlayerPrefs.SetInt("FrameRate", 60); break;
            case 2: frameRateManager.GetComponent<LimitFps>().SetRate(90); PlayerPrefs.SetInt("FrameRate", 90); break;
            case 3: frameRateManager.GetComponent<LimitFps>().SetRate(120); PlayerPrefs.SetInt("FrameRate", 120); break;
            case 4: frameRateManager.GetComponent<LimitFps>().SetRate(240); PlayerPrefs.SetInt("FrameRate", 240); break;
            case 5: frameRateManager.GetComponent<LimitFps>().SetRate(-1); PlayerPrefs.SetInt("FrameRate", -1); break;
        }
    }
    public void VsyncToggle()
    {
        frameRateManager.GetComponent<LimitFps>().ToggleVSync(VSync.GetComponent<Toggle>().isOn);
    }
    public void Exit()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
}
