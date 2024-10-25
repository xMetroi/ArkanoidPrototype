using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBarrier : MonoBehaviour
{
    [Header("Death Barrier Properties")]
    [SerializeField] private float damage;
    
    private void OnTriggerEnter(Collider coll)
    {
        if (!coll.CompareTag("Ball"))
            return;

        IDamageable damageable = GameObject.FindWithTag("Player").GetComponent<IDamageable>();
        
        damageable.Damage(damage);
    }
}
