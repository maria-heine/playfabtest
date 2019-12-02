using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoreStateView : GameStateView
{
    [Header("Store Screen")]
    [SerializeField] private GameObject _storeScreen;
    [SerializeField] private GridLayoutGroup _storeGrid;
    [SerializeField] private GameObject _listedStoreItemPrefab;
    [SerializeField] private Button _goBackButton;
    [SerializeField] private TextMeshProUGUI _cashText;

    [Header("Loading screen")]
    [SerializeField] private GameObject _storeLoadingScreen;

    [Header("Purchasing item screen")]
    [SerializeField] private GameObject _purchasingItemScreen;

    public Action GoBackButtonPressed;
    public Action<string, int> StoreItemPressed;

    private void Awake()
    {
        _goBackButton.onClick.AddListener(() =>
        {
            GoBackButtonPressed.Invoke();
        });
    }

    private void Start()
    {
        ClearStore();
        DisplayStore(false);
        //_storeScreen.SetActive(false);
        //_storeLoadingScreen.SetActive(true);
        TogglePurchasingScreen(false);
    }

    public void DisplayStore(bool display)
    {
        _storeLoadingScreen.SetActive(!display);
        _storeScreen.SetActive(display);
    }

    public void ClearStore()
    {
        _storeGrid.Clear();
    }

    public void DisplayStoreItems(StoreItem itemData)
    {
        StoreItemView itemView = GameObject
            .Instantiate(_listedStoreItemPrefab, _storeGrid.transform, false)
            .GetComponent<StoreItemView>();

        itemView.SetItemID(itemData.ID);

        itemView.Pressed += itemID =>
        {
            StoreItemPressed
                .Invoke(itemID, (int)itemData.VirtualCurrencyPrices["SC"]);
        };

        // clean up long references?
        string itemTitle = GameController
            .Instance
            .CommonItemsStore
            .GetItem(itemData.ID)
            .Title;

        string itemDescription = GameController
            .Instance
            .CommonItemsStore
            .GetItem(itemData.ID)
            .Description;

        itemView.SetItemTitle(itemTitle);
        itemView.SetItemPrice((int)itemData.VirtualCurrencyPrices["SC"]);
        itemView.SetItemDescription(itemDescription);
    }

    public void DisplayPlayerCash(int cash)
    {
        _cashText.text = $"Your cash: {cash} SC";
    }

    public void TogglePurchasingScreen(bool toggle)
    {
        _purchasingItemScreen.SetActive(toggle);
    }
}
