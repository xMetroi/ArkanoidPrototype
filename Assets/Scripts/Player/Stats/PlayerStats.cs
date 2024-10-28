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
    
    public void Damage(float damage)
    {
        SetPlayerHp(playerHp - damage);
        OnPlayerDamaged?.Invoke(playerHp);

        if (GetPlayerHp() <= 0)
            OnPlayerDeath?.Invoke();
    }

    public void AddGamePoints(int points)
    {
        gamePoints += points;
        OnGamePointAdded?.Invoke(points);
    }
}
