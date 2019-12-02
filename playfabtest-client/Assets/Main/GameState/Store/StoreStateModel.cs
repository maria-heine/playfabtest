using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreStateModel : GameStateModel
{
    public GenericDataStore<string, StoreItem> StoreItems { get; private set; } =
        new GenericDataStore<string, StoreItem>();

}
