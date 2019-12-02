using System;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public IWebService WebService { get; private set; }

    public GenericDataStore<string, CommonItem> CommonItemsStore;
    public GameState CurrentGameState { get; private set; }
    public static GameController Instance { get; private set; }
    public PlayerController PlayerController { get; set; }

    /* entity hacks */
    public Dictionary<string, string> EntityTokens { get; set; } 

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        WebService = new PlayfabWebService();
        EntityTokens = new Dictionary<string, string>();

        SetState(typeof(LoginState));
    }

    public void PostLoginInitialization()
    {
        StartInitializationChain();
    }

    #region INITIALIZATION

    private void StartInitializationChain()
    {
        InitializeItemStore();
    }

    private void InitializeItemStore()
    {
        CommonItemsStore = new GenericDataStore<string, CommonItem>();

        GetCommonItemsRequestData requestData = new GetCommonItemsRequestData
        {
            CatalogVersion = "main",
            DataStore = CommonItemsStore
        };

        WebService.GetCommonItems(requestData, OnItemStoreInitialized);
    }
    private void OnItemStoreInitialized(CommandResult result)
    {
        //CommonItemsStore.QueryItems(item => Debug.Log(item.ID));
        RequestUpdatePlayerInventory(OnPlayerInventoryInitialized);
    }

    private void OnPlayerInventoryInitialized()
    {
        EndInitializationChain();
    }

    private void EndInitializationChain()
    {
        SetState(typeof(LobbyState));
    }

    #endregion

    #region STATE MANAGEMENT
    public async void SetState(Type stateType)
    {
        GameState state = null;

        if (stateType == typeof(LoginState))
        {
            GameStateModel model = GameStateFactory
                .Instance
                .CreateModel<LoginStateModel>();
            GameStateView view = await GameStateFactory
                .Instance
                .CreateViewAsync<LoginStateView>("LoginStateView");
            LoginState loginState = GameStateFactory
                .Instance
                .CreateState<LoginState>(model, view);
            state = loginState;
        }
        else if (stateType == typeof(LobbyState))
        {
            GameStateModel model = GameStateFactory
                .Instance
                .CreateModel<LobbyStateModel>();
            GameStateView view = await GameStateFactory
                .Instance
                .CreateViewAsync<LobbyStateView>("LobbyStateView");
            LobbyState lobbyState = GameStateFactory
                .Instance
                .CreateState<LobbyState>(model, view);
            state = lobbyState;
        }
        else if (stateType == typeof(InventoryState))
        {
            GameStateModel model = GameStateFactory
                .Instance
                .CreateModel<InventoryStateModel>();
            GameStateView view = await GameStateFactory
                .Instance
                .CreateViewAsync<InventoryStateView>("InventoryStateView");
            InventoryState inventoryState = GameStateFactory
                .Instance
                .CreateState<InventoryState>(model, view);
            state = inventoryState;
        }
        else if (stateType == typeof(StoreState))
        {
            GameStateModel model = GameStateFactory
                .Instance
                .CreateModel<StoreStateModel>();
            GameStateView view = await GameStateFactory
                .Instance
                .CreateViewAsync<StoreStateView>("StoreStateView");
            StoreState storeState = GameStateFactory
                .Instance
                .CreateState<StoreState>(model, view);
            state = storeState;
        }
        else if (stateType == typeof(IngameState))
        {
            GameStateModel model = GameStateFactory
                .Instance
                .CreateModel<IngameStateModel>();
            GameStateView view = await GameStateFactory
                .Instance
                .CreateViewAsync<IngameStateView>("IngameStateView");
            IngameState ingameState = GameStateFactory
                .Instance
                .CreateState<IngameState>(model, view);
            state = ingameState;
        }
        else if (stateType == typeof(LeaderboardState))
        {
            GameStateModel model = GameStateFactory
                .Instance
                .CreateModel<LeaderboardStateModel>();
            GameStateView view = await GameStateFactory
                .Instance
                .CreateViewAsync<LeaderboardStateView>("LeaderboardStateView");
            LeaderboardState leaderboardState = GameStateFactory
                .Instance
                .CreateState<LeaderboardState>(model, view);
            state = leaderboardState;
        }
        else
        {
            // do the catch
            Debug.LogError("Setting state for an unknown state type " + stateType.GetType());
        }

        if (state != null)
        {
            SwitchState(state);
        }
    }

    private void SwitchState(GameState newState)
    {
        CurrentGameState?.End();
        CurrentGameState = newState;
        CurrentGameState.Begin();
    }

    #endregion

    #region PLAYER MANAGEMENT

    public void InstantiatePlayerContoller(string playerId)
    {
        PlayerController = new PlayerController(new PlayerModel(playerId));
    }

    public void RequestUpdatePlayerInventory(Action Callback = null)
    {
        var data = new GetInventoryRequestData
        {
            DataStore = PlayerController.Model.PlayerInventory.Items,
            VirtualCurrency = PlayerController.Model.PlayerInventory.VirtualCurrency
        };

        Action<CommandResult> OnUpdated = (CommandResult result) =>
        {
            result.TryOnResult<GetInventoryCommandResult>((resultData) =>
            {
                PlayerController.Model.PlayerInventory.VirtualCurrency = resultData.VirtualCurrency;

                Callback?.Invoke();
            });
        };

        WebService.GetInventoryItems(data, OnUpdated);
    }

    #endregion

}
