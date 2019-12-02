using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyState : GameState
{
    private readonly IWebService _webService;
    private readonly LobbyStateView _view;
    private readonly LobbyStateModel _model;
    public LobbyState(GameStateModel model, GameStateView view) : base(model, view)
    {
        _webService = GameController.Instance.WebService;
        _view = View as LobbyStateView;
        _model = Model as LobbyStateModel;
    }

    public override void Begin()
    {
        Debug.Log("Begin lobby");
        GetModelData();
        _view.OpenInventoryButtonPressed += OnOpenInventoryButtonPressed;
        _view.OpenStoreButtonPressed += OnOpenStoreButtonPressed;
        _view.OpenLeaderboardButtonPressed += OnLeaderboardButtonPressed;
        _view.PlayButtonPressed += OnPlayButtonPressed;
    }

    #region STATE CHANGES

    private void OnOpenInventoryButtonPressed()
    {
        GameController.Instance.SetState(typeof(InventoryState));
    }

    private void OnOpenStoreButtonPressed()
    {
        GameController.Instance.SetState(typeof(StoreState));
    }

    private void OnPlayButtonPressed()
    {
        GameController.Instance.SetState(typeof(IngameState));
    }

    private void OnLeaderboardButtonPressed()
    {
        GameController.Instance.SetState(typeof(LeaderboardState));
    }

    #endregion

    #region WEB SERVICE REQUESTS
    private void GetModelData()
    {
        // Question: what if I require multiple PlayFab requests to launch a certain
        // state, how would I wait for all requests and merge them?
        GetAccountInfoRequestData data = new GetAccountInfoRequestData
        {
            PlayFabId = PlayerModel.PlayFabId
        };
        _webService.GetAccountInfo(data, OnGetAccountInfo);
    }

    private void OnGetAccountInfo(CommandResult result)
    {
        result.TryOnResult<GetAccountInfoCommandResult>(expectedResult =>
        {
            _model.Username = expectedResult.Username;

            GetPlayerXp();
        });
    }

    private void GetPlayerXp()
    {
        GetPlayerStatisticsRequestData data = new GetPlayerStatisticsRequestData
        {
            StatisticsNames = new List<string> { "playerXp" }
        };
        _webService.GetPlayerStatistics(data, OnGotPlayerXp);
    }

    private void OnGotPlayerXp(CommandResult result)
    {
        result.TryOnResult<GetPlayerStatisticsCommandResult>(expectedResult =>
        {
            int playerXp;

            try
            {
                playerXp = expectedResult.StatisticsDictionary["playerXp"];
            }
            catch (KeyNotFoundException)
            {
                playerXp = 0;
            }

            _model.PlayerXP = playerXp;

            //MakeTestEntityRequest();
            SetupLobbyStateView();
        });
    }

    private void MakeTestEntityRequest()
    {
        _webService.MakeTestEntityRequest(new EntityTestRequestData(), (result) => { });
    }

    #endregion

    #region VIEW CALLS

    private void SetupLobbyStateView()
    {
        _view.SetPlayerWelcomeText(_model.GetPlayerWelcomeText());
        _view.SetPlayerXpText(_model.PlayerXP);
        _view.ToggleLobbyScreen(true);
    }

    #endregion  

    public override void End()
    {
        GameObject.Destroy(View.gameObject);
        // is unregistering to events necessary, this stade will get garbage collected
        // anyway once it is overwritten in GameController
    }
}
