using PlayFab;
using PlayFab.ClientModels;


public class BuyItemRequestData : WebRequestData
{
    public string ItemID { get; set; }
    public int Price { get; set; }
    public string CurrencyType { get; set; }
}

public class BuyItemCommandArgs : CommandArgs
{
    public string ItemID { get; set; }
    public int Price { get; set; }
    public string CurrencyType { get; set; }
}

public class BuyItemCommandResult : CommandResult
{

}

public class BuyItemCommand : PlayFabAPICommand
{
    public override void SetExpectedTypes()
    {
        _expectedCommandArgsType = typeof(BuyItemCommandArgs);
    }

    public override void Execute()
    {
        if (_args is BuyItemCommandArgs buyItemArgs)
        {
            var request = new PurchaseItemRequest
            { 
                ItemId = buyItemArgs.ItemID,
                Price = buyItemArgs.Price,
                VirtualCurrency = buyItemArgs.CurrencyType
            };

            PlayFabClientAPI.PurchaseItem(request, OnItemPurchased, OnHttpError);
        }
    }

    private void OnItemPurchased(PurchaseItemResult result)
    {
        BuyItemCommandResult buyItemResult = new BuyItemCommandResult
        {
        };

        Callback?.Invoke(buyItemResult);
    }
}
