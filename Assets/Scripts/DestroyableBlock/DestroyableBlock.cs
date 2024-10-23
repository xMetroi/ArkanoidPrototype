using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableBlock : MonoBehaviour, IDamageable
{
    [Header("DestroyableBlock Properties")] 
    [SerializeField] private float blockHp;

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
            OnBlockDeath?.Invoke();
        }
    }
    
}
