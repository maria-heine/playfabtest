using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GetCommonItemsRequestData : WebRequestData
{
    public string CatalogVersion { get; set; }
    public GenericDataStore<string, CommonItem> DataStore { get; set; }
}

public class GetCommonItemsCommandArgs : CommandArgs
    //where T : PlayfabItemBase
{
    public string CatalogVersion { get; set; }
    public GenericDataStore<string, CommonItem> DataStore { get; set; }

}

public class GetCommonItemsCommandResult : CommandResult
{
    // empty
}

public class GetCommonItemsCommand : PlayFabAPICommand
    //where T : PlayfabItemBase
{
    public override void SetExpectedTypes()
    {
        _expectedCommandArgsType = typeof(GetCommonItemsCommandArgs);
    }

    public override void Execute()
    {
        if (_args is GetCommonItemsCommandArgs getCatalogArgs)
        {
            var request = new GetCatalogItemsRequest
            {
                CatalogVersion = getCatalogArgs.CatalogVersion
            };

            PlayFabClientAPI.GetCatalogItems(request, OnGotCatalog, OnHttpError);
        }
    }

    private void OnGotCatalog(GetCatalogItemsResult result)
    {
        GetCommonItemsCommandArgs localArgs = _args as GetCommonItemsCommandArgs;

        foreach(CatalogItem catalogItem in result.Catalog)
        {
            localArgs
                .DataStore
                .SetItem(catalogItem.ItemId, new CommonItem
                {
                    ID = catalogItem.ItemId,
                    Title = catalogItem.DisplayName,
                    Description = catalogItem.Description,
                    ImageUrl = catalogItem.ItemImageUrl
                });
        }
        GetCommonItemsCommandResult getCatalogItemsResult = new GetCommonItemsCommandResult
        {
            // empty
        };

        Callback?.Invoke(getCatalogItemsResult);
    }
}
