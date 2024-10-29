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
    
    public void AddLifes(float lifesToAdd)
    {
        references.playerStats.SetPlayerHp(references.playerStats.GetPlayerHp() + lifesToAdd);
        
        OnAddLife?.Invoke();
    }

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
