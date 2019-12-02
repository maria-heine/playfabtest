using PlayFab;
using PlayFab.ClientModels;
using PlayFab.DataModels;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EntityTestRequestData : WebRequestData
{
}

public class EntityTestCommandArgs : CommandArgs
{
}

public class EntityTestCommandResult : CommandResult
{
}

public class EntityTestCommand : PlayFabAPICommand
{
    public override void SetExpectedTypes()
    {
        _expectedCommandArgsType = typeof(EntityTestCommandArgs);
    }

    public override void Execute()
    {
        Debug.Log("wehere");
        var data = new Dictionary<string, object>()
        {
            {"Health", 100},
            {"Mana", 10000}
        };

        var dataList = new List<SetObject>()
        {
            new SetObject()
            {
                ObjectName = "PlayerData",
                DataObject = data
            }
        };

        if (_args is EntityTestCommandArgs getAccountInfoArgs)
        {
            string entityType = "title_player_account";

            var request = new SetObjectsRequest
            {
                Entity = new PlayFab.DataModels.EntityKey
                {
                    Type = entityType,
                    Id = GameController.Instance.EntityTokens[entityType]
                },
                Objects = dataList
            };

            PlayFabDataAPI.SetObjects(request, OnSetObjects, OnHttpError);
        }
    }

    private void OnSetObjects(SetObjectsResponse result)
    {
        Debug.Log(result.ProfileVersion);
        Debug.Log(result.ToString());
        //Debug.Log(setResult.ProfileVersion);

        GetAccountInfoCommandResult getAccountInfoResult = new GetAccountInfoCommandResult
        {
            //Username = result.AccountInfo.Username
        };

        Callback?.Invoke(getAccountInfoResult);
    }
}
