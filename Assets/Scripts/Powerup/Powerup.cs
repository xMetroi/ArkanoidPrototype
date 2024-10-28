using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{

    enum PowerupType
    {
        Resize,
        BallSpeed,
        PaddleSpeed,
        Life,
    }
    
    [Header("Movement Properties")]
    [SerializeField] private float movementSpeed;
    [SerializeField] private PowerupType type;
    
    [Header("Resize Properties")]
    [SerializeField] private Vector2 resizeMultiplier;
    [SerializeField] private float resizeTime;
    
    [Header("SpeedMultiplier Properties")]
    [SerializeField] private float speedMultiplier;
    [SerializeField] private float speedMultiplierTime;
    
    [Header("Ball Speed Multiplier Properties")]
    [SerializeField] private float ballSpeedMultiplier;
    [SerializeField] private float ballSpeedMultiplierTime;
    
    [Header("Add life Properties")]
    [SerializeField] private float lifeToAdd;
    
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * movementSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;
        
        IPowerup powerup = other.GetComponent<IPowerup>();
        
        switch (type)
        {
            case PowerupType.Resize:
                powerup.Resize(resizeMultiplier.x, resizeMultiplier.y, resizeTime);
                break;
            case PowerupType.BallSpeed:
                powerup.BallSpeedMultiplier(ballSpeedMultiplier, ballSpeedMultiplierTime);
                break;
            case PowerupType.PaddleSpeed:
                powerup.SpeedMultiplier(speedMultiplier, speedMultiplierTime);
                break;
            case PowerupType.Life:
                powerup.AddLifes(lifeToAdd);
                break;
        }
        
        Destroy(gameObject);
    }
}
