using System;

public class PlayerModel
{
    // is this static a good idea
    public static string PlayFabId { get; private set; }

    public PlayerInventory PlayerInventory { get; set; }

    public PlayerModel(string playFabId)
    {
        if (string.IsNullOrEmpty(playFabId))
            throw new ArgumentException("Tried to initialize PlayerModel with a null or empty PlayFabId");
        
        PlayFabId = playFabId;
        PlayerInventory = new PlayerInventory();
    }
}
