using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MixerController : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    private float savedValue;

    public string typeCheck;
    private void Start()
    {
        if (typeCheck == "Master")
        {
            savedValue = PlayerPrefs.GetFloat("SavedMasterVolume", 1);
            GetComponent<Slider>().value = PlayerPrefs.GetFloat("SavedMasterVolume", 1);
            _audioMixer.SetFloat("MasterVolume", Mathf.Log10(savedValue) * 20);
        }
        if (typeCheck == "Music")
        {
            savedValue = PlayerPrefs.GetFloat("SavedMusicVolume", 1);
            GetComponent<Slider>().value = PlayerPrefs.GetFloat("SavedMusicVolume", 1);
            _audioMixer.SetFloat("MusicVolume", Mathf.Log10(savedValue) * 20);
        }
        if (typeCheck == "SFX")
        {
            savedValue = PlayerPrefs.GetFloat("SavedSFXVolume", 1);
            GetComponent<Slider>().value = PlayerPrefs.GetFloat("SavedSFXVolume", 1);
            _audioMixer.SetFloat("SFXVolume", Mathf.Log10(savedValue) * 20);
        }
    }
    public void SetMasterVolume(float sliderValue)
    {
        _audioMixer.SetFloat("MasterVolume", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("SavedMasterVolume", sliderValue);
    }
    public void SetMusicVolume(float sliderValue)
    {
        _audioMixer.SetFloat("MusicVolume", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("SavedMusicVolume", sliderValue);
    }
    public void SetSFXVolume(float sliderValue)
    {
        _audioMixer.SetFloat("SFXVolume", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("SavedSFXVolume", sliderValue);
    }
}
