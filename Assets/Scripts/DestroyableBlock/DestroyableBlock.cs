using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DestroyableBlock : MonoBehaviour, IDamageable
{
    [Header("DestroyableBlock Properties")] 
    [SerializeField] private float blockHp;
    
    [Header("Powerup Properties")]
    [SerializeField] private GameObject[] powerups;

    #region Events
    
    public event Action<float, float> OnBlockHit;
    public event Action OnBlockDeath;
    
    #endregion
    
    #region Getter / Setter
    
    public void SetBlockHp(float hp) { blockHp = hp; }
    public float GetBlockHp() { return blockHp; }
    
    #endregion
    
    /// <summary>
    /// IDamageable damage method
    /// </summary>
    /// <param name="damage"></param>
    public void Damage(float damage)
    {
        SetBlockHp(blockHp - damage);
        OnBlockHit?.Invoke(damage, blockHp);

        if (GetBlockHp() <= 0)
        {
            Destroy(gameObject);
            SpawnRandomPowerup(0.5f); // 50% probability of spawn
            OnBlockDeath?.Invoke();
        }
    }
    
    #region Utilities

    /// <summary>
    /// Spawns a random powerup from the list
    /// </summary>
    private void SpawnRandomPowerup(float probability)
    {
        if (Random.value <= probability)
        {
            Instantiate(powerups[Random.Range(0, powerups.Length)], transform.position, Quaternion.identity);
        }
    }
    
    #endregion
}
