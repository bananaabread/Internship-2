using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitFps : MonoBehaviour
{
    [SerializeField] private int maxFrameRate = 60;
    private bool vSyncEnabled = false;

    public void Awake()
    {
        DontDestroyOnLoad(this);

        if (!vSyncEnabled)
        {
            Application.targetFrameRate = maxFrameRate;
        }
        else
        {
            QualitySettings.vSyncCount = 1;
        }
    }
    public void Update()
    {
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

    public void ToggleVSync()
    {
        if (vSyncEnabled)
        {
            vSyncEnabled = false;
        }
        else if (!vSyncEnabled)
        {
            vSyncEnabled = true;
        }
    }
}
