using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory
{
    public GenericDataStore<string, InventoryItem> Items { get; private set; } =
        new GenericDataStore<string, InventoryItem>();

    public Dictionary<string, int> VirtualCurrency { get; set; } =
        new Dictionary<string, int>();
}
