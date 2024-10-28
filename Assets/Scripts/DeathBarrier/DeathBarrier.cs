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
        
        GameObject player = GameObject.FindWithTag("Player");
        
        //Apply damage to the player life
        IDamageable damageable = player.GetComponent<IDamageable>();
        
        damageable.Damage(damage);
        
        //Remove the ball from the ball list
        GameManager.Instance.GetBallsList().Remove(coll.gameObject);
        
        PlayerStats playerStats = player.GetComponent<PlayerStats>();
        
        //if there are no more balls on the stage damage the player and the player still have lives
        if (GameManager.Instance.GetBallsList().Count <= 0 && playerStats.GetPlayerHp() > 0)
        {
            //Spawn another ball
            Vector3 ballSpawnPoint = GameObject.Find("BallSpawnPoint").transform.position;

            GameManager.Instance.SpawnBall(ballSpawnPoint, 2);
        }
    }
}
