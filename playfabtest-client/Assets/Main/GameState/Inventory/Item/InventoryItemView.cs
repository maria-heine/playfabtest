using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryItemView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _itemTitle;
    [SerializeField] private TextMeshProUGUI _itemDescription;
    [SerializeField] private TextMeshProUGUI _itemCount;

    public void SetItemTitle(string title)
    {
        _itemTitle.text = title;
    }

    public void SetItemDescription(string description)
    {
        _itemDescription.text = description;
    }

    public void SetItemCount(int? count)
    {
        _itemCount.text = "x" + count.ToString();
    }
}
