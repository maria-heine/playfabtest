using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GetAccountInfoRequestData : WebRequestData
{
    public string PlayFabId { get; set; }
}

public class GetAccountInfoCommandArgs : CommandArgs
{
    public string PlayFabId { get; set; }
}

public class GetAccountInfoCommandResult : CommandResult
{
    public string Username { get; set; }
}

public class GetAccountInfoCommand : PlayFabAPICommand
{
    public override void SetExpectedTypes()
    {
        _expectedCommandArgsType = typeof(GetAccountInfoCommandArgs);
    }

    public override void Execute()
    {
        if (_args is GetAccountInfoCommandArgs getAccountInfoArgs)
        {
            var request = new GetAccountInfoRequest
            {
                PlayFabId = getAccountInfoArgs.PlayFabId
            };

            PlayFabClientAPI.GetAccountInfo(request, OnGotAccountInfo, OnHttpError);
        }
    }

    private void OnGotAccountInfo(GetAccountInfoResult result)
    {
        GetAccountInfoCommandResult getAccountInfoResult = new GetAccountInfoCommandResult
        {
            Username = result.AccountInfo.Username
        };

        Callback?.Invoke(getAccountInfoResult);
    }
}
