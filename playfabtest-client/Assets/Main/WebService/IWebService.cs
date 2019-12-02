using System;

/* 
 * Fields, 
 * Constructors, 
 * Properties, 
 * Events, 
 * Methods, 
 * Private interface implementations, 
 * Nested types
 */

public abstract class WebRequestData { }

public interface IWebService
{
    void Login(WebRequestData args, Action<CommandResult> Callback);
    void BuyItem(WebRequestData args, Action<CommandResult> Callback);
    void GetStoreItems(WebRequestData args, Action<CommandResult> Callback);
    void GetCommonItems(WebRequestData args, Action<CommandResult> Callback);
    void GetAccountInfo(WebRequestData args, Action<CommandResult> Callback);
    void GetLeaderboard(WebRequestData args, Action<CommandResult> Callback);
    void RegisterWithEmail(WebRequestData args, Action<CommandResult> Callback);
    void GetInventoryItems(WebRequestData args, Action<CommandResult> Callback);
    void ExecuteServerScript(WebRequestData args, Action<CommandResult> Callback);
    void GetPlayerStatistics(WebRequestData args, Action<CommandResult> Callback);
    void MakeTestEntityRequest(WebRequestData args, Action<CommandResult> Callback);

}
