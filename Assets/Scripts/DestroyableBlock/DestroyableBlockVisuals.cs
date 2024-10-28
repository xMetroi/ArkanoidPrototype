using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableBlockVisuals : MonoBehaviour
{
    private DestroyableBlock destroyableBlock;
    
    private void Start()
    {
        destroyableBlock = GetComponent<DestroyableBlock>();

        destroyableBlock.OnBlockHit += OnBlockHitted;
        
        ColorManager(destroyableBlock.GetBlockHp());
    }

    private void OnDestroy()
    {
        if (destroyableBlock != null)
            destroyableBlock.OnBlockHit -= OnBlockHitted;
    }

    private void OnBlockHitted(float damage, float blockHp)
    {
        ColorManager(blockHp);
    }

    private void ColorManager(float blockHp)
    {
        switch (blockHp)
        {
            case 3:
                GetComponentInChildren<Renderer>().material.color = Color.green;
                break;
            case 2:
                GetComponentInChildren<Renderer>().material.color = Color.yellow;
                break;
            case 1:
                GetComponentInChildren<Renderer>().material.color = Color.red;
                break;
        }
    }
}
