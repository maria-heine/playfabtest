using PlayFab;
using PlayFab.ClientModels;


public class GetStoreItemsRequestData : WebRequestData
{
    public string CatalogVersion { get; set; }
    public string StoreId { get; set; }
    public GenericDataStore<string, StoreItem> DataStore { get; set; }
}

public class GetStoreItemsCommandArgs : CommandArgs
{
    public string CatalogVersion { get; set; }
    public string StoreId { get; set; }
    public GenericDataStore<string, StoreItem> DataStore { get; set; }

}

public class GetStoreItemsCommandResult : CommandResult
{
    // empty
}

public class GetStoreItemsCommand : PlayFabAPICommand
{
    public override void SetExpectedTypes()
    {
        _expectedCommandArgsType = typeof(GetStoreItemsCommandArgs);
    }

    public override void Execute()
    {
        if (_args is GetStoreItemsCommandArgs storeArgs)
        {
            var request = new GetStoreItemsRequest
            {
                CatalogVersion = storeArgs.CatalogVersion,
                StoreId = storeArgs.StoreId
            };

            PlayFabClientAPI.GetStoreItems(request, OnGotCatalog, OnHttpError);
        }
    }

    private void OnGotCatalog(GetStoreItemsResult result)
    {
        GetStoreItemsCommandArgs localArgs = _args as GetStoreItemsCommandArgs;

        foreach (PlayFab.ClientModels.StoreItem storeItem in result.Store)
        {
            localArgs
                .DataStore
                .SetItem(storeItem.ItemId, new StoreItem
                {
                    ID = storeItem.ItemId,
                    VirtualCurrencyPrices = storeItem.VirtualCurrencyPrices
                });
        }
        GetStoreItemsCommandResult getStoreItemsResult = new GetStoreItemsCommandResult
        {
            // empty
        };

        Callback?.Invoke(getStoreItemsResult);
    }
}
