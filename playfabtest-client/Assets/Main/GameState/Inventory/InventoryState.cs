using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryState : GameState
{
    private readonly IWebService _webService;
    private readonly InventoryStateView _view;
    private readonly InventoryStateModel _model;

    public InventoryState(GameStateModel model, GameStateView view) : base(model, view)
    {
        _webService = GameController.Instance.WebService;
        _view = View as InventoryStateView;
        _model = Model as InventoryStateModel;
    }

    public override void Begin()
    {
        RequestPlayerInventory();
        BindViewEvents();
    }

    private void BindViewEvents()
    {
        _view.GoBackButtonPressed += () =>
        {
            GameController.Instance.SetState(typeof(LobbyState));
        };
    }

    private void RequestPlayerInventory()
    {
        var data = new GetInventoryRequestData
        {
            DataStore = _model.InventoryItems
        };
        _webService.GetInventoryItems(data, OnGotInventoryItems);
    }

    private void OnGotInventoryItems(CommandResult result)
    {
        result.TryOnResult<GetInventoryCommandResult>((resultData) =>
        {
            DisplayInventory();
        });
    }

    private void DisplayInventory()
    {
        _view.ClearInventory();
        _model.InventoryItems.QueryItems(_view.DisplayInventoryItem);
        _view.DisplayInventory(); 
    }

    public override void End()
    {
        GameObject.Destroy(_view.gameObject);
        Debug.Log("End inventory");
    }
}
