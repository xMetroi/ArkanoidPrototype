using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPowerup
{
    void Resize(float xMultiplier, float yMultiplier, float duration);
    void SpeedMultiplier(float speedMultiplier, float duration);

    void BallSpeedMultiplier(float ballSpeedMultiplier, float duration);

    void AddLifes(float lifesToAdd);

    void BallMultiplier(int ballMultiplier);
}
