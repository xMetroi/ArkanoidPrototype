using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour, IDamageable
{
    [Header("Player Stats")] 
    [SerializeField] private float playerHp;
    [SerializeField] private float gamePoints;
    
    #region Events

    public event Action<float> OnPlayerDamaged; 
    public event Action OnPlayerDeath;
    public event Action<float> OnGamePointAdded;
    
    #endregion
    
    #region Getter / Setter
    
    public void SetPlayerHp(float value) { playerHp = value; }
    public float GetPlayerHp() { return playerHp; }
    
    public void SetGamePoints(float value) { gamePoints = value; }
    public float GetGamePoints() { return gamePoints; }
    
    #endregion
    
    /// <summary>
    /// Method to damage player
    /// </summary>
    /// <param name="damage"> quantity of damage to be applied </param>
    public void Damage(float damage)
    {
        SetPlayerHp(playerHp - damage);
        OnPlayerDamaged?.Invoke(playerHp);

        if (GetPlayerHp() <= 0)
            OnPlayerDeath?.Invoke();
    }

    /// <summary>
    /// Method to add game points to player
    /// </summary>
    /// <param name="points"> quantity of points to be added </param>
    public void AddGamePoints(int points)
    {
        gamePoints += points;
        OnGamePointAdded?.Invoke(points);
    }
}
