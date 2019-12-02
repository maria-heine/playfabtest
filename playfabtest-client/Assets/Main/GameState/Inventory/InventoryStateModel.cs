using System.Collections.Generic;

public class InventoryStateModel : GameStateModel
{
    public GenericDataStore<string, InventoryItem> InventoryItems { get; private set; } =
        new GenericDataStore<string, InventoryItem>();

    public Dictionary<string, int> VirtualCurrency { get; set; } =
        new Dictionary<string, int>();
}
