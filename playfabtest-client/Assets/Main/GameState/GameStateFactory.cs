using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class GameStateFactory
{
    public static GameStateFactory Instance { get; } = new GameStateFactory();

    #region CREATE MODEL

    public TModel CreateModel<TModel>()
        where TModel : GameStateModel, new()
    {
        GameStateModel model = ScriptableObject.CreateInstance<TModel>();
        return model as TModel;
    }

    #endregion

    #region CREATE VIEW

    public async Task<TView> CreateViewAsync<TView>(string addressableAssetName)
        where TView : GameStateView, new()
    {
        var viewAsset = await InstantiateViewAsset(addressableAssetName);

        TView view = viewAsset.GetComponent<TView>();

        if (view == null)
        {
            throw new ArgumentNullException(
                "GameStateFactory: asset does not have expected GameStateView component at address: " +
                addressableAssetName);
        }

        return view;
    }

    private async Task<GameObject> InstantiateViewAsset(string addressableAssetName)
    {
        var operationHandle = Addressables.InstantiateAsync(addressableAssetName);

        GameObject result = await operationHandle
            .Task
            .ContinueWith(x => { return x.Result; });

        if (result == null)
        {
            throw new ArgumentException(
                "GameStateFactory: failed loading View for the following address: " + 
                addressableAssetName);
        }

        return result;
    }

    #endregion

    #region CREATE STATE

    public TState CreateState<TState>(GameStateModel model, GameStateView view)
        where TState : GameState
    {
        // Activator used due to limitations of accessing constructors in generic methods
        // (where only parameterless constructor calls are allowed)
        TState state = (TState)Activator.CreateInstance(typeof(TState), model, view);
        return state;
    }

    #endregion
}
