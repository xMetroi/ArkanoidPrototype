using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource musicSource;

    [Header("Music Clips")]
    [SerializeField] private AudioClip lobbyMusic;
    [SerializeField] private AudioClip gameMusic;
    
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
    
    /// <summary>
    /// Reproduce a one shot in the SFX Audio Source
    /// </summary>
    /// <param name="clip"> audioclip to be reproduced </param>
    public void PlaySFXOneShot(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
    
    #endregion
    
    #region Music Source

    /// <summary>
    /// Reproduce a one shot in the Music Audio Source
    /// </summary>
    /// <param name="clip"> audioclip to be reproduced </param>
    public void PlayMusicOneShot(AudioClip clip)
    {
        musicSource.PlayOneShot(clip);
    }

    /// <summary>
    /// Play the lobby music
    /// </summary>
    public void PlayLobbyMusic()
    {
        musicSource.clip = lobbyMusic;
        musicSource.Play();
    }

    /// <summary>
    /// Play the game music
    /// </summary>
    public void PlayGameMusic()
    {
        musicSource.clip = gameMusic;
        musicSource.Play();
    }

    /// <summary>
    /// Stop the audio clip playing in the Music Audio Source
    /// </summary>
    public void StopMusic()
    {
        musicSource.Stop();
    }
    
    #endregion
}
