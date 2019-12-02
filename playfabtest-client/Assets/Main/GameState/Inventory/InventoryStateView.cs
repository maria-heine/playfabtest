using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryStateView : GameStateView
{
    [Header("Inventory Screen")]
    [SerializeField] private GameObject _inventoryScreen;
    [SerializeField] private GridLayoutGroup _inventoryGrid;
    [SerializeField] private GameObject _listedItemPrefab;
    [SerializeField] private Button _goBackButton;

    [Header("Inventory Loading")]
    [SerializeField] private GameObject _inventoryLoadingScreen;

    public Action GoBackButtonPressed;

    private void Awake()
    {
        ClearInventory();

        _goBackButton.onClick.AddListener(() =>
        {
            GoBackButtonPressed?.Invoke();
        });
    }

    private void Start()
    {
        _inventoryScreen.SetActive(false);
        _inventoryLoadingScreen.SetActive(true);
    }

    public void DisplayInventory()
    {
        _inventoryLoadingScreen.SetActive(false);
        _inventoryScreen.SetActive(true);
    }
    
    public void ClearInventory()
    {
        _inventoryGrid.Clear();
    }

    public void DisplayInventoryItem(InventoryItem itemData)
    {
        var itemPrefab = GameObject
            .Instantiate(_listedItemPrefab, _inventoryGrid.transform, false)
            .GetComponent<InventoryItemView>();

        //Debug.Log(itemData.ID);

        // LIKE REALLY, CLEAN THIS UP
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

        itemPrefab.SetItemTitle(itemTitle);
        itemPrefab.SetItemCount(itemData.Count);
        itemPrefab.SetItemDescription(itemDescription);
    }
}
