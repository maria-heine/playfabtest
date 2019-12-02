using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreState : GameState
{
    private readonly IWebService _webService;
    private readonly StoreStateView _view;
    private readonly StoreStateModel _model;

    public StoreState(GameStateModel model, GameStateView view) : base(model, view)
    {
        _webService = GameController.Instance.WebService;
        _view = View as StoreStateView;
        _model = Model as StoreStateModel;
    }

    public override void Begin()
    {
        Debug.Log("Begin store");
        BindViewEvents();
        RequestStoreData();
    }

    private void BindViewEvents()
    {
        _view.GoBackButtonPressed += () =>
        {
            GameController.Instance.SetState(typeof(LobbyState));
        };

        _view.StoreItemPressed += OnStoreItemPressed;
    }

    #region WEB REQUESTS

    private void RequestStoreData()
    {
        var data = new GetStoreItemsRequestData
        {
            CatalogVersion = "main",
            StoreId = "brokenteethStore",
            DataStore = _model.StoreItems
        };

        _webService.GetStoreItems(data, OnGetStoreItems);
    }

    private void OnGetStoreItems(CommandResult result)
    {
        result.TryOnResult<GetStoreItemsCommandResult>((expectedResult) =>
        {
            DisplayStore();
        });
    }

    private void OnStoreItemPressed(string itemID, int itemPrice)
    {
        var data = new BuyItemRequestData
        {
            ItemID = itemID,
            Price = itemPrice,
            CurrencyType = "SC"
        };

        _view.TogglePurchasingScreen(true);
        _webService.BuyItem(data, OnItemBought);
    }

    private void OnItemBought(CommandResult result)
    {
        result.TryOnResult<BuyItemCommandResult>((expectedResult) =>
        {
            GameController
                    .Instance
                    .RequestUpdatePlayerInventory(OnPlayerInventoryUpdated);
        });
    }

    private void OnPlayerInventoryUpdated()
    {
        UpdatePlayerCashDispaly();
        _view.TogglePurchasingScreen(false);
    }

    #endregion

    private void DisplayStore()
    {
        Debug.Log("Display store");

        UpdatePlayerCashDispaly();
        _model.StoreItems.QueryItems(_view.DisplayStoreItems);
        _view.DisplayStore(true);
    }

    private void UpdatePlayerCashDispaly()
    {
        int cash;

        if (GameController
            .Instance
            .PlayerController
            .Model
            .PlayerInventory
            .VirtualCurrency
            .TryGetValue("SC", out cash))
        {
            _view.DisplayPlayerCash(cash);
        }
        else
        {
            Debug.LogError("wtf");
        }        
    }

    public override void End()
    {
        Debug.Log("End store");
        GameObject.Destroy(_view.gameObject);
    }
}
