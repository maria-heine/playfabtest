using System;
using UnityEngine;

public class LoginState : GameState
{
    private readonly IWebService _webService;
    private readonly LoginStateView _view;
    private readonly LoginStateModel _model;

    public LoginState(GameStateModel model, GameStateView view) : base(model, view)
    {
        _webService = GameController.Instance.WebService;
        _view = View as LoginStateView;
        _model = Model as LoginStateModel;
    }

    public override void Begin()
    {
        _view.LoginButtonClicked += RegisterLoginButton;
        _view.RegisterButtonClicked += RegisterRegisterButton;

        // REMOVE THIS HACK
        //RegisterLoginButton(new LoginRequestData
        //{
        //    Email = "dd@d.com",
        //    Password = "hey5512"
        //});
    }

    public override void End()
    {
        _view.LoginButtonClicked -= RegisterLoginButton;
        _view.RegisterButtonClicked -= RegisterRegisterButton;

        GameObject.Destroy(_view.gameObject);
    }

    // Rework again with the additional step in between
    private void RegisterLoginButton(LoginRequestData data)
    {
        _webService.Login(data, OnLoginOrRegisterResult);
    }

    private void RegisterRegisterButton(RegisterRequestData data)
    {
        _webService.RegisterWithEmail(data, OnLoginOrRegisterResult);
    }

    private void OnLoginOrRegisterResult(CommandResult result)
    {
        if (result.CommandResultType == CommandResultType.Success)
        {
            switch (result)
            {
                case LoginCommandResult loginResult:
                    GameController
                        .Instance
                        .InstantiatePlayerContoller(loginResult.PlayFabId);

                    // entity hacks
                    GameController
                        .Instance
                        .EntityTokens
                        .Add(loginResult.EntityType, loginResult.EntityId);

                    break;
                case RegisterCommandResult registerResult:
                    GameController
                        .Instance
                        .InstantiatePlayerContoller(registerResult.PlayFabId);
                    break;
                default:
                    throw new ArgumentException("Unxpected CommandResult of type " + result.GetType());
            }

            GameController.Instance.PostLoginInitialization();
        }
        else if (result.CommandResultType == CommandResultType.Error)
        {
            _view.DisplayErrorMessage(true, result.ErrorMessage);
        }
        else
        {
            throw new ArgumentException("Enexpected CommandResultType enum: " +
                result.CommandResultType.ToString());
        }
    }
}