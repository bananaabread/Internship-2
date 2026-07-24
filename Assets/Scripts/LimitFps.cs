using System.Collections;
using System.Collections.Generic;
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
    }
    public void Update()
    {
        VSync = GameObject.FindGameObjectWithTag("VSync");
        //if (VSync != null)
        //{
        //    if (VSync.activeInHierarchy == true)
        //    {
        //        if (vSyncEnabled && VSync.GetComponent<Toggle>() != null)
        //        {
        //            VSync.GetComponent<Toggle>().isOn = true;
        //        }
        //        if (!vSyncEnabled && VSync.GetComponent<Toggle>() != null)
        //        {
        //            VSync.GetComponent<Toggle>().isOn = false;
        //        }
        //    }
        //}
        if (VSync != null && !hasSetVSync)
        {
            VSync.GetComponent<Toggle>().isOn = vSyncEnabled;
            hasSetVSync = true;
        }
        if (VSync == null && hasSetVSync)
        {
            hasSetVSync = false;
        }
        if (!vSyncEnabled)
        {
            Application.targetFrameRate = maxFrameRate;
            QualitySettings.vSyncCount = 0;
        }
        if (vSyncEnabled)
        {
            Application.targetFrameRate = -1;
            QualitySettings.vSyncCount = 1;
        }
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
