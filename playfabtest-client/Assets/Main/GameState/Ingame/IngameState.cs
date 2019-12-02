using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngameState : GameState
{
    private readonly IWebService _webService;
    private readonly IngameStateView _view;
    private readonly IngameStateModel _model;

    public IngameState(GameStateModel model, GameStateView view) : base(model, view)
    {
        _webService = GameController.Instance.WebService;
        _view = View as IngameStateView;
        _model = Model as IngameStateModel;
    }

    public override void Begin()
    {
        _view.gameObject.SetActive(true);
        SubscribeToViewEvents();
    }

    private void SubscribeToViewEvents()
    {
        _view.OnGameTimeUpdated += OnGameTimeUpdated;
        _view.PlayerKilledAnEnemy += OnPlayerKilledAnEnemy;
    }

    private void UnsubscribeToViewEvents()
    {
        _view.OnGameTimeUpdated -= OnGameTimeUpdated;
        _view.PlayerKilledAnEnemy -= OnPlayerKilledAnEnemy;
    }

    private void OnPlayerKilledAnEnemy()
    {
        _model.AddPlayerKill();
        _view.UpdateKillCounter(_model.KillCount);
    }

    private void OnGameTimeUpdated(float time)
    {
        if(_model.IsTimeUp(time, out float timeLeft))
        {
            EndSession();
        }
        else
        {
            _view.UpdateGameTimeDisplayer(timeLeft);
        }
    }

    private void EndSession()
    {
        UnsubscribeToViewEvents();

        _view.TogglePlayerCube(false);
        _view.DisplayEndScreen(_model.KillCount);

        var data = new ExecuteServerScriptRequestData
        {
            FunctionName = "grantXp",
            AnonymousParameter = new { killCount = _model.KillCount },
            ExpectExecutionConfirmation = true
        };

        _webService.ExecuteServerScript(data, OnGrantedPlayerXp);
    }

    private void OnGrantedPlayerXp(CommandResult result)
    {
        _view.ToggleEndGameButton(true);
        _view.EndGameButtonPressed += () =>
        {
            GameController.Instance.SetState(typeof(LobbyState));
        };
    }

    public override void End()
    {
        GameObject.Destroy(_view.gameObject);
    }
}
