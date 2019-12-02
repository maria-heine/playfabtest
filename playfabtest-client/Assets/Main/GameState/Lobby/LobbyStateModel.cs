using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyStateModel : GameStateModel
{
    public readonly string playerWelcomePrefix = "Welcome player ";
    public string Username { get; set; }
    public int PlayerXP { get; set; }

    public string GetPlayerWelcomeText()
    {
        return playerWelcomePrefix + Username;
    }
}
