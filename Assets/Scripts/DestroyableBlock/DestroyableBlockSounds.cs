using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableBlockSounds : MonoBehaviour
{
    [Header("Sounds")]
    [SerializeField] private AudioClip blockHitClip;
    [SerializeField] private AudioClip blockDeathClip;
    
    private DestroyableBlock destroyableBlock;
    
    // Start is called before the first frame update
    void Start()
    {
        destroyableBlock = GetComponent<DestroyableBlock>();
        
        destroyableBlock.OnBlockHit += OnBlockHit;
        destroyableBlock.OnBlockDeath += OnBlockDeath;
    }

    /// <summary>
    /// Triggers when a destroyable block is impacted
    /// </summary>
    /// <param name="damage"> damage received </param>
    /// <param name="hp"> actualHp </param>
    private void OnBlockHit(float damage, float hp)
    {
        SoundManager.Instance.PlaySFXOneShot(blockHitClip);
    }

    /// <summary>
    /// Triggers when a destroyable block is destroyed ( reach zero hp )
    /// </summary>
    private void OnBlockDeath()
    {
        SoundManager.Instance.PlaySFXOneShot(blockDeathClip);
    }
}
