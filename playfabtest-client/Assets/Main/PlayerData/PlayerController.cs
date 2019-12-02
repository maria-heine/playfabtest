using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController
{
    public PlayerModel Model { get; private set; }

    public PlayerController(PlayerModel model)
    {
        Model = model;
    }
}
