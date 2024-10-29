using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource musicSource;
    
    public static SoundManager Instance;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    #region SFX Source
    
    public void PlaySFXOneShot(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
    
    #endregion
    
    #region Music Source

    public void PlayMusicOneShot(AudioClip clip)
    {
        musicSource.PlayOneShot(clip);
    }
    
    #endregion
}
