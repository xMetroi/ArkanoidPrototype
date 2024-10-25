using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour, IDamageable
{
    [Header("Player Stats")] 
    [SerializeField] private float playerHp;
    
    #region Events

    public event Action<float> OnPlayerDamaged; 
    public event Action OnPlayerDeath; 
    
    #endregion
    
    #region Getter / Setter
    
    public void SetPlayerHp(float value) { playerHp = value; }
    public float GetPlayerHp() { return playerHp; }
    
    #endregion

    public void Damage(float damage)
    {
        SetPlayerHp(playerHp - damage);
        OnPlayerDamaged?.Invoke(playerHp);
        
        if (GetPlayerHp() <= 0)
            OnPlayerDeath?.Invoke();
    }
}
