using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSounds : MonoBehaviour
{
    [Header("Ball Sounds")]
    [SerializeField] private AudioClip ballImpactClip;
    
    private BallMovement ballMovement;
    
    // Start is called before the first frame update
    void Start()
    {
        ballMovement = GetComponent<BallMovement>();
        
        ballMovement.OnBallImpact += OnBallImpact;
    }

    private void OnDestroy()
    {
        if (ballMovement != null)
        {
            ballMovement.OnBallImpact -= OnBallImpact;
        }
    }

    private void OnBallImpact()
    {
        SoundManager.Instance.PlaySFXOneShot(ballImpactClip);
    }
}
