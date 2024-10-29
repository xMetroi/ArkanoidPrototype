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
    
    /// <summary>
    /// Triggers when player gets a resize powerup
    /// </summary>
    private void OnResize()
    {
        SoundManager.Instance.PlaySFXOneShot(powerupClip);
    }

    /// <summary>
    /// Triggers when player gets a speed multiplier powerup
    /// </summary>
    private void OnSpeedMultiplier()
    {
        SoundManager.Instance.PlaySFXOneShot(powerupClip);
    }

    /// <summary>
    /// Triggers when player gets a ball speed multiplier powerup
    /// </summary>
    private void OnBallSpeedMultiplier()
    {
        SoundManager.Instance.PlaySFXOneShot(powerupClip);
    }

    /// <summary>
    /// Triggers when player gets an add life powerup / bonus
    /// </summary>
    private void OnAddLife()
    {
        SoundManager.Instance.PlaySFXOneShot(powerupClip);
    }
    
    #endregion
    
    #region Player Stats
    
    /// <summary>
    /// Triggers when player is damaged ( the ball reach the death barrier )
    /// </summary>
    /// <param name="damage"> damage applied </param>
    private void OnPlayerDamaged(float damage)
    {
        SoundManager.Instance.PlaySFXOneShot(playerDamageClip);
    }

    /// <summary>
    /// Triggers when player is dead ( no more lifes remaining )
    /// </summary>
    private void OnPlayerDeath()
    {
        SoundManager.Instance.PlaySFXOneShot(playerDeathClip);
    }

    /// <summary>
    /// Triggers when a game point is added ( when a destroyable block is destroyed )
    /// </summary>
    /// <param name="pointsAdded"> quantity of points added </param>
    private void OnGamePointAdded(float pointsAdded)
    {
        
    }
    
    #endregion
    
    #region Level

    /// <summary>
    /// Triggers when the level is passed successfully ( all the destroyable blocks are destroyed )
    /// </summary>
    private void OnLevelPassed()
    {
        SoundManager.Instance.PlayMusicOneShot(levelPassedClip);
    }
    
    #endregion
}
