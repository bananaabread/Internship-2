using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LimitFps : MonoBehaviour
{
    [SerializeField] private int maxFrameRate = 60;
    private bool vSyncEnabled = false;

    public static LimitFps Instance;

    private GameObject VSync;
    private bool hasSetVSync = false;

    public GameObject settingsPanel;

    [Header("Check for overlay")]
    public bool hasSeenMenu = false;
    public GameObject MainMenu;

    public void Start()
    {
        if (PlayerPrefs.GetInt("VSyncOn", 1) == 0)
        {
            vSyncEnabled = false;
        }
        if (PlayerPrefs.GetInt("VSyncOn", 1) == 1)
        {
            vSyncEnabled = true;
        }
        VSync = GameObject.FindGameObjectWithTag("VSync");
        if (VSync != null && !hasSetVSync)
        {
            VSync.GetComponent<Toggle>().isOn = vSyncEnabled;
            hasSetVSync = true;
        }
        SetRate(PlayerPrefs.GetInt("FrameRate", 60));
        if (!hasSeenMenu && MainMenu != null)
        {
            MainMenu.GetComponent<MainMenuScript>().DestroyOverlay();
        }
    }
    public void Awake()
    {
        DontDestroyOnLoad(this);

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        if (!hasSeenMenu && MainMenu == null)
        {
            hasSeenMenu = true;
        }
    }
    public void Update()
    {
        if (!vSyncEnabled)
        {
            Application.targetFrameRate = maxFrameRate;
            Debug.Log(Application.targetFrameRate);
            QualitySettings.vSyncCount = 0;
        }
        if (vSyncEnabled)
        {
            Application.targetFrameRate = -1;
            QualitySettings.vSyncCount = 1;
        }
        Debug.Log(Application.targetFrameRate);
        Debug.Log(QualitySettings.vSyncCount);
    }

    public void seenMenu()
    {
        hasSeenMenu = true;
    }
    public void SetRate(int value)
    {
        maxFrameRate = value;
    }

    public void ToggleVSync(bool toggle)
    {
        vSyncEnabled = toggle;
        if (vSyncEnabled)
        {
            PlayerPrefs.SetInt("VSyncOn", 1);
        }
        if (!vSyncEnabled)
        {
            PlayerPrefs.SetInt("VSyncOn", 0);
        }
    }
}
