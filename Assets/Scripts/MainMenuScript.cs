using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    [Header ("Settings")]
    public GameObject Settings;
    public GameObject FrameRateOption;
    private GameObject frameRateManager;
    public GameObject VSync;
    
    private bool settingsEnabled = false;
    private float DisTarg;
    private float DisStart;
    public float speed = 5f;

    public Transform settingsStart;
    public Transform settingsTarg;

    [Header("HighScore")]
    public GameObject HighScorePanel;

    private bool highScoreEnabled = false;

    [Header ("Buttons")]
    public GameObject SoloButton;
    public GameObject VsButton;
    public GameObject SettingsButton;
    public GameObject HighScoreButton;
    public GameObject QuitButton;
    public GameObject SoloButtonReal;
    public GameObject VsButtonReal;
    public GameObject SettingsButtonReal;
    public GameObject HighScoreButtonReal;
    public GameObject QuitButtonReal;

    [Header ("Audio")]
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
        //Settings.SetActive(false);
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
    private void Update()
    {
        DisTarg = Vector2.Distance(Settings.transform.position, settingsTarg.position);
        DisStart = Vector2.Distance(Settings.transform.position, settingsStart.position);
        if (VSync.GetComponent<Toggle>().isOn)
        {
            FrameRateOption.GetComponent<TMP_Dropdown>().interactable = false;
        }
        if (!VSync.GetComponent<Toggle>().isOn)
        {
            FrameRateOption.GetComponent<TMP_Dropdown>().interactable = true;
        }
        switch (settingsEnabled)
        {
            case true:
                if (DisTarg > 0)
                {
                    Settings.transform.position = Vector3.MoveTowards(Settings.transform.position, settingsTarg.position, speed * Time.deltaTime);
                }
                break;
            case false:
                if (DisStart > 0)
                {
                    Settings.transform.position = Vector3.MoveTowards(Settings.transform.position, settingsStart.position, speed * Time.deltaTime);
                }
                break;
        }
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
        settingsEnabled = true;

        SoloButton.GetComponent<ButtonScript>().enabled = false;
        VsButton.GetComponent<ButtonScript>().enabled = false;
        SettingsButton.GetComponent<ButtonScript>().enabled = false;
        HighScoreButton.GetComponent<ButtonScript>().enabled = false;
        QuitButton.GetComponent<ButtonScript>().enabled = false;
        SoloButtonReal.GetComponent<Button>().enabled = false;
        VsButtonReal.GetComponent<Button>().enabled = false;
        SettingsButtonReal.GetComponent<Button>().enabled = false;
        HighScoreButtonReal.GetComponent<Button>().enabled = false;
        QuitButtonReal.GetComponent<Button>().enabled = false;
    }
    public void CloseSettings()
    {
        settingsEnabled = false;

        SoloButton.GetComponent<ButtonScript>().enabled = true;
        VsButton.GetComponent<ButtonScript>().enabled = true;
        SettingsButton.GetComponent<ButtonScript>().enabled = true;
        HighScoreButton.GetComponent<ButtonScript>().enabled = true;
        QuitButton.GetComponent<ButtonScript>().enabled = true;
        SoloButtonReal.GetComponent<Button>().enabled = true;
        VsButtonReal.GetComponent<Button>().enabled = true;
        SettingsButtonReal.GetComponent<Button>().enabled = true;
        HighScoreButtonReal.GetComponent<Button>().enabled = true;
        QuitButtonReal.GetComponent<Button>().enabled = true;
    }
    public void ShowHighScore()
    {
        highScoreEnabled = true;

        HideHighScore(); //Temporary, remove when high score panel is added

        //SoloButton.GetComponent<ButtonScript>().enabled = false;
        //VsButton.GetComponent<ButtonScript>().enabled = false;
        //SettingsButton.GetComponent<ButtonScript>().enabled = false;
        //HighScoreButton.GetComponent<ButtonScript>().enabled = false;
        //QuitButton.GetComponent<ButtonScript>().enabled = false;
        //SoloButtonReal.GetComponent<Button>().enabled = false;
        //VsButtonReal.GetComponent<Button>().enabled = false;
        //SettingsButtonReal.GetComponent<Button>().enabled = false;
        //HighScoreButtonReal.GetComponent<ButtonScript>().enabled = false;
        //QuitButtonReal.GetComponent<Button>().enabled = false;
    }
    public void HideHighScore()
    {
        highScoreEnabled = false;

        //SoloButton.GetComponent<ButtonScript>().enabled = true;
        //VsButton.GetComponent<ButtonScript>().enabled = true;
        //SettingsButton.GetComponent<ButtonScript>().enabled = true;
        //HighScoreButton.GetComponent<ButtonScript>().enabled = true;
        //QuitButton.GetComponent<ButtonScript>().enabled = true;
        //SoloButtonReal.GetComponent<Button>().enabled = true;
        //VsButtonReal.GetComponent<Button>().enabled = true;
        //SettingsButtonReal.GetComponent<Button>().enabled = true;
        //HighScoreButtonReal.GetComponent<ButtonScript>().enabled = true;
        //QuitButtonReal.GetComponent<Button>().enabled = true;
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
