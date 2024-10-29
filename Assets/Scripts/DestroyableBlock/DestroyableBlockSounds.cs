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

    private void OnBlockHit(float damage, float hp)
    {
        SoundManager.Instance.PlaySFXOneShot(blockHitClip);
    }

    private void OnBlockDeath()
    {
        SoundManager.Instance.PlaySFXOneShot(blockDeathClip);
    }
}
