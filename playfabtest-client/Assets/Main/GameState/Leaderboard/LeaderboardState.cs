using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardState : GameState
{
    private readonly IWebService _webService;
    private readonly LeaderboardStateView _view;
    private readonly LeaderboardStateModel _model;

    public LeaderboardState(LeaderboardStateModel model, LeaderboardStateView view) : base(model, view)
    {
        _webService = GameController.Instance.WebService;
        _view = View as LeaderboardStateView;
        _model = Model as LeaderboardStateModel;
    }

    public override void Begin()
    {
        Debug.Log("Begin leaderboard");
        GetLeaderboardData();

        _view.GoBackButtonPressed += () =>
        {
            GameController.Instance.SetState(typeof(LobbyState));
        };
    }

    private void GetLeaderboardData()
    {
        var data = new GetLeaderBoardRequestData
        {
            LeaderboardKey = "playerXp",
            StartingFrom = 0,
            MaxResultsCount = 5
        };

        _webService.GetLeaderboard(data, OnGotLeaderboard);
    }

    private void OnGotLeaderboard(CommandResult result)
    {
        result.TryOnResult<GetLeaderBoardCommandResult>((resultData) =>
        {
            _model.SetLeaderBoard(resultData.LeaderBoard);
            _model.QueryLeaderboard(_view.LoadLeaderboardScore);
            _view.DisplayLeaderboard();
        });
    }

    public override void End()
    {
        GameObject.Destroy(View.gameObject);
        Debug.Log("End leaderboard");
    }
}
