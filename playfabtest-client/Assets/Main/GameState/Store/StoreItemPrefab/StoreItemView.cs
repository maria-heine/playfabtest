using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;
using System;

public class StoreItemView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _itemTitle;
    [SerializeField] private TextMeshProUGUI _itemDescription;
    [SerializeField] private TextMeshProUGUI _itemPrice;
    [SerializeField] private Button _itemButton;

    // Should separate this into model/view
    public string ID { get; private set; }

    public Action<string> Pressed;

    private void Awake()
    {
        _itemButton.onClick.AddListener(() =>
       {
           Debug.Log($"Store item {ID} pressed");
           Pressed?.Invoke(ID);
       });
    }

    public void SetItemID(string ID)
    {
        this.ID = ID;
    }

    public void SetItemTitle(string title)
    {
        _itemTitle.text = title;
    }

    public void SetItemDescription(string description)
    {
        _itemDescription.text = description;
    }

    public void SetItemPrice(int price)
    {
        _itemPrice.text = price.ToString() + " SC";
    }
}
