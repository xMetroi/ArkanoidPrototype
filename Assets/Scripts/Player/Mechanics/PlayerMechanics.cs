using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMechanics : MonoBehaviour, IPowerup
{
    private PlayerReferences references;
    
    #region Events

    public event Action OnResize;
    public event Action OnSpeedMultiplier;
    public event Action OnBallSpeedMultiplier;
    public event Action OnAddLife;

    public event Action OnBallMultiplier;
    
    #endregion
    
    void Start()
    {
        references = GetComponent<PlayerReferences>();
    }
    
    #region Powerups

    /// <summary>
    /// Resize the player bar
    /// </summary>
    /// <param name="xMultiplier"> size multiplier in X </param>
    /// <param name="yMultiplier"> size multiplier in Y </param>
    /// <param name="duration"> duration of the powerup </param>
    public void Resize(float xMultiplier, float yMultiplier, float duration)
    {
        StartCoroutine(ResizeCoroutine(xMultiplier, yMultiplier, duration));
    }

    private IEnumerator ResizeCoroutine(float xMultiplier, float yMultiplier, float duration)
    {
        Vector3 startScale = transform.localScale;
        
        transform.localScale = new Vector3(transform.localScale.x * xMultiplier, transform.localScale.y * yMultiplier, transform.localScale.z);
        
        OnResize?.Invoke();
        
        yield return new WaitForSeconds(duration);
        
        transform.localScale = startScale;
    }

    /// <summary>
    /// Multiplies the speed of the player
    /// </summary>
    /// <param name="speedMultiplier"> value to be multiplied by </param>
    /// <param name="duration"> duration of the powerup </param>
    public void SpeedMultiplier(float speedMultiplier, float duration)
    {
        StartCoroutine(SpeedMultiplierCoroutine(speedMultiplier, duration));
    }
    
    private IEnumerator SpeedMultiplierCoroutine(float speedMultiplier, float duration)
    {
        float startSpeed = references.playerMovement.GetMovementSpeed();
        
        references.playerMovement.SetMovementSpeed(startSpeed * speedMultiplier);
        
        OnSpeedMultiplier?.Invoke();
        
        yield return new WaitForSeconds(duration);
        
        references.playerMovement.SetMovementSpeed(startSpeed);
    }
    
    /// <summary>
    /// Multiplies the speed of the balls
    /// </summary>
    /// <param name="ballSpeedMultiplier"> value to be multiplied by </param>
    /// <param name="duration"> duration of the powerup </param>
    public void BallSpeedMultiplier(float ballSpeedMultiplier, float duration)
    {
        StartCoroutine(BallSpeedMultiplierCoroutine(ballSpeedMultiplier, duration));
    }
    
    private IEnumerator BallSpeedMultiplierCoroutine(float ballSpeedMultiplier, float duration)
    {
        BallMovement[] ballMovement = FindObjectsOfType<BallMovement>();
        
        float startSpeed = ballMovement[0].GetMovementSpeed();
        
        for (int i = 0; i < ballMovement.Length; i++)
        {
            ballMovement[i].SetMovementSpeed(startSpeed * ballSpeedMultiplier);
        }
        
        OnBallSpeedMultiplier?.Invoke();
        
        yield return new WaitForSeconds(duration);
        
        for (int i = 0; i < ballMovement.Length; i++)
        {
            ballMovement[i].SetMovementSpeed(startSpeed);
        }
        
    }
    
    /// <summary>
    /// Add lifes to the player
    /// </summary>
    /// <param name="lifesToAdd"> number of lifes to be added </param>
    public void AddLifes(float lifesToAdd)
    {
        references.playerStats.SetPlayerHp(references.playerStats.GetPlayerHp() + lifesToAdd);
        
        OnAddLife?.Invoke();
    }

    /// <summary>
    /// Multiplies the quantity of balls in the level
    /// </summary>
    /// <param name="ballMultiplier"> value to be multiplied by </param>
    public void BallMultiplier(int ballMultiplier)
    {
        for (int i = 0; i < ballMultiplier; i++)
        {
            Transform ballPosition = GameObject.FindAnyObjectByType<BallMovement>().transform;
            GameManager.Instance.SpawnBall(ballPosition.position, 0.2f);
        }
        
        OnBallMultiplier?.Invoke();
    }

    #endregion
}
