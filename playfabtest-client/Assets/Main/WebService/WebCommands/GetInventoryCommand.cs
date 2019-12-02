using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GetInventoryRequestData : WebRequestData
{
    public GenericDataStore<string, InventoryItem> DataStore { get; set; }

    public Dictionary<string, int> VirtualCurrency { get; set; }

}

public class GetInventoryCommandArgs : CommandArgs
{
    public GenericDataStore<string, InventoryItem> DataStore { get; set; }
    public Dictionary<string, int> VirtualCurrency { get; set; }

}

public class GetInventoryCommandResult : CommandResult
{
    public Dictionary<string, int> VirtualCurrency { get; set; }

}

public class GetInventoryCommand : PlayFabAPICommand
{
    public override void SetExpectedTypes()
    {
        _expectedCommandArgsType = typeof(GetInventoryCommandArgs);
    }

    public override void Execute()
    {
        if (_args is GetInventoryCommandArgs getInventoryArgs)
        {
            var request = new GetUserInventoryRequest();

            PlayFabClientAPI.GetUserInventory(request, OnGotUserInventory, OnHttpError);
        }
    }

    private void OnGotUserInventory(GetUserInventoryResult result)
    {
        GetInventoryCommandArgs localArgs = _args as GetInventoryCommandArgs;

        foreach (ItemInstance itemInstance in result.Inventory)
        {
            localArgs.DataStore.SetItem(itemInstance.ItemId, new InventoryItem
            {
                ID = itemInstance.ItemId,
                Count = itemInstance.RemainingUses
            });
        }

        //localArgs.VirtualCurrency = result.VirtualCurrency;

        GetInventoryCommandResult getInventoryResult = new GetInventoryCommandResult
        {
            VirtualCurrency = result.VirtualCurrency
        };

        Callback?.Invoke(getInventoryResult);
    }
}
