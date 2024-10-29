using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenuManager : MonoBehaviour
{
    [Header("Audio Properties")]
    [SerializeField] private AudioMixer audioMixer;

    /// <summary>
    /// Set the volume of the master in the slider
    /// </summary>
    /// <param name="sliderValue"> value of the slider </param>
    public void SetMasterVolume(float sliderValue)
    {
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(sliderValue) * 20);
    }
    
    /// <summary>
    /// Set the volume of the sfx in the slider
    /// </summary>
    /// <param name="sliderValue"> value of the slider </param>
    public void SetSFXVolume(float sliderValue)
    {
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(sliderValue) * 20);
    }
    
    /// <summary>
    /// Set the volume of the music in the slider
    /// </summary>
    /// <param name="sliderValue"> value of the slider </param>
    public void SetMusicVolume(float sliderValue)
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(sliderValue) * 20);
    }
}
