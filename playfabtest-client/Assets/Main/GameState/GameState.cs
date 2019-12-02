using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameStateModel : ScriptableObject { }

public abstract class GameStateView : MonoBehaviour { }

public abstract class GameState
{
    public GameStateModel Model { get; protected set; }
    public GameStateView View { get; protected set; }

    public GameState(GameStateModel model, GameStateView view)
    {
        Model = model;
        View = view;
    }

    public abstract void Begin();

    public abstract void End();
}

