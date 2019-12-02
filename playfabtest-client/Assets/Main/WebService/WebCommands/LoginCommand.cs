using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

public class LoginRequestData : WebRequestData
{
    // Like is this step even necessary, could we just use LoginCommandArgs
    public string Email { get; set; }
    public string Password { get; set; }
}

public class LoginCommandArgs : CommandArgs
{
    public string Email { get; set; }
    public string Password { get; set; }
}

public class LoginCommandResult : CommandResult
{
    public string PlayFabId { get; set; }
    public string EntityId { get; set; }
    public string EntityType { get; set; }

}

public class LoginCommand : PlayFabAPICommand
{
    public override void Execute()
    {
        var args = _args as LoginCommandArgs;
        var request = new LoginWithEmailAddressRequest()
        {
            Email = args.Email,
            Password = args.Password,
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
            {
                GetCharacterInventories = true
            }
        };

        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoggedIn, OnHttpError);
    }

    public override void SetExpectedTypes()
    {
        _expectedCommandArgsType = typeof(LoginCommandArgs);
    }

    private void OnLoggedIn(LoginResult result)
    {
        Debug.Log("entity rtokem " + result.EntityToken.Entity.Type);
        LoginCommandResult loginCommandResult = new LoginCommandResult
        {
            PlayFabId = result.PlayFabId,
            EntityId = result.EntityToken.Entity.Id,
            EntityType = result.EntityToken.Entity.Type
        };
        Callback?.Invoke(loginCommandResult);
    }
}
