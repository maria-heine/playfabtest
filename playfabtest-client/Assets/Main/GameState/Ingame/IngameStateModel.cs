using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngameStateModel : GameStateModel
{
    public const float MISSION_TIME = 30f;
    public int KillCount { get; private set; }

    public IngameStateModel()
    {
        KillCount = 0;
    }

    public void AddPlayerKill()
    {
        KillCount += 1;
    }

    public bool IsTimeUp(float currentTime, out float timeLeft)
    {
        if (currentTime >= MISSION_TIME)
        {
            timeLeft = 0f;
            return true;
        }
        else
        {
            timeLeft = MISSION_TIME - currentTime;
            return false;
        }
    }
}
