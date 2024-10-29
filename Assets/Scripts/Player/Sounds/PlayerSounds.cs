using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    [Header("Powerups Sounds")]
    [SerializeField] private AudioClip powerupClip;

    [Header("Player Stats")]
    [SerializeField] private AudioClip playerDamageClip;
    [SerializeField] private AudioClip playerDeathClip;
    
    [Header("Levels Sounds")]
    [SerializeField] private AudioClip levelPassedClip;
    
    private PlayerReferences references;
    
    void Start()
    {
        references = GetComponent<PlayerReferences>();
        
        //Powerups Events
        references.playerMechanics.OnResize += OnResize;
        references.playerMechanics.OnSpeedMultiplier += OnSpeedMultiplier;
        references.playerMechanics.OnBallSpeedMultiplier += OnBallSpeedMultiplier;
        references.playerMechanics.OnAddLife += OnAddLife;
        
        //Player Stats Events
        references.playerStats.OnPlayerDamaged += OnPlayerDamaged;
        references.playerStats.OnPlayerDeath += OnPlayerDeath;
        references.playerStats.OnGamePointAdded += OnGamePointAdded;
        
        //Level Sounds
        GameManager.Instance.OnLevelPassed += OnLevelPassed;
    }

    private void OnDestroy()
    {
        if (references != null)
        {
            //Powerups Events
            references.playerMechanics.OnResize -= OnResize;
            references.playerMechanics.OnSpeedMultiplier -= OnSpeedMultiplier;
            references.playerMechanics.OnBallSpeedMultiplier -= OnBallSpeedMultiplier;
            references.playerMechanics.OnAddLife -= OnAddLife;
        
            //Player Stats Events
            references.playerStats.OnPlayerDamaged -= OnPlayerDamaged;
            references.playerStats.OnPlayerDeath -= OnPlayerDeath;
            references.playerStats.OnGamePointAdded -= OnGamePointAdded;
            GameManager.Instance.OnLevelPassed -= OnLevelPassed;
        }
    }
    
    #region Powerups
    
    private void OnResize()
    {
        SoundManager.Instance.PlaySFXOneShot(powerupClip);
    }

    private void OnSpeedMultiplier()
    {
        SoundManager.Instance.PlaySFXOneShot(powerupClip);
    }

    private void OnBallSpeedMultiplier()
    {
        SoundManager.Instance.PlaySFXOneShot(powerupClip);
    }

    private void OnAddLife()
    {
        SoundManager.Instance.PlaySFXOneShot(powerupClip);
    }
    
    #endregion
    
    #region Player Stats
    
    private void OnPlayerDamaged(float damage)
    {
        SoundManager.Instance.PlaySFXOneShot(playerDamageClip);
    }

    private void OnPlayerDeath()
    {
        SoundManager.Instance.PlaySFXOneShot(playerDeathClip);
    }

    private void OnGamePointAdded(float pointsAdded)
    {
        
    }
    
    #endregion
    
    #region Level

    private void OnLevelPassed()
    {
        SoundManager.Instance.PlayMusicOneShot(levelPassedClip);
    }
    
    #endregion
}
