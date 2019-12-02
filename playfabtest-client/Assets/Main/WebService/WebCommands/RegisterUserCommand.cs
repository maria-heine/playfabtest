using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

public class RegisterRequestData : WebRequestData
{
    public string RegisterUsername { get; set; }
    public string RegisterEmail { get; set; }
    public string RegisterPassword { get; set; }
}

public class RegisterCommandArgs : CommandArgs
{
    public string RegisterUsername { get; set; }
    public string RegisterEmail { get; set; }
    public string RegisterPassword { get; set; }
}

public class RegisterCommandResult : CommandResult
{
    public string PlayFabId { get; set; }
}

public class RegisterCommand : PlayFabAPICommand
{
    public override void SetExpectedTypes()
    {
        _expectedCommandArgsType = typeof(RegisterCommandArgs);
    }

    public override void Execute()
    {
        if (_args is RegisterCommandArgs registerArgs)
        {
            var request = new RegisterPlayFabUserRequest
            {
                Username = registerArgs.RegisterUsername,
                Email = registerArgs.RegisterEmail,
                Password = registerArgs.RegisterPassword,
                DisplayName = registerArgs.RegisterUsername
            };

            PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisteredPlayFabUser, OnHttpError);
        }
    }

    private void OnRegisteredPlayFabUser(RegisterPlayFabUserResult result)
    {
        RegisterCommandResult registerUserResult = new RegisterCommandResult
        {

            PlayFabId = result.PlayFabId
        };

        Callback?.Invoke(registerUserResult);
    }
}
