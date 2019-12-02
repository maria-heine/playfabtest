using System;

public class PlayfabWebService : IWebService
{
    public void Login(WebRequestData args, Action<CommandResult> Callback)
    {
        if(args is LoginRequestData loginArgs)
        {
            CommandArgs commandArgs = new LoginCommandArgs
            {
                Callback = Callback,
                Email = loginArgs.Email,
                Password = loginArgs.Password
            };

            LoginCommand command = CommandFactory
                .Instance
                .CreateCommand<LoginCommand>(commandArgs);
            command.Execute();
        }
        else
        {
            // Make more verbose exceptions
            throw new ArgumentException("Incorrect args data type;");
        }
    }

    public void RegisterWithEmail(WebRequestData args, Action<CommandResult> Callback)
    {
        if (args is RegisterRequestData registerArgs)
        {
            CommandArgs commandArgs = new RegisterCommandArgs
            {
                Callback = Callback,
                RegisterUsername = registerArgs.RegisterUsername,
                RegisterEmail = registerArgs.RegisterEmail,
                RegisterPassword = registerArgs.RegisterPassword
            };

            RegisterCommand command = CommandFactory
                .Instance
                .CreateCommand<RegisterCommand>(commandArgs);
            command.Execute();
        }
        else
        {
            // Make more verbose exceptions
            throw new ArgumentException("Incorrect args data type;");
        }
    }

    public void GetAccountInfo(WebRequestData args, Action<CommandResult> Callback)
    {
        if (args is GetAccountInfoRequestData getAccountInfoArgs)
        {
            CommandArgs commandArgs = new GetAccountInfoCommandArgs
            {
                Callback = Callback,
                PlayFabId = getAccountInfoArgs.PlayFabId
            };

            GetAccountInfoCommand command = CommandFactory
                .Instance
                .CreateCommand<GetAccountInfoCommand>(commandArgs);
            command.Execute();
        }
        else
        {
            // Make more verbose exceptions
            throw new ArgumentException("Incorrect args data type;");
        }
    }

    public void GetInventoryItems(WebRequestData args, Action<CommandResult> Callback)
    {
        if (args is GetInventoryRequestData localArgs)
        {
            CommandArgs commandArgs = new GetInventoryCommandArgs
            {
                Callback = Callback,
                DataStore = localArgs.DataStore,
                VirtualCurrency = localArgs.VirtualCurrency
            };

            GetInventoryCommand command = CommandFactory
                .Instance
                .CreateCommand<GetInventoryCommand>(commandArgs);
            command.Execute();
        }
        else
        {
            // Make more verbose exceptions
            throw new ArgumentException("Incorrect args data type;");
        }
    }

    public void GetCommonItems(WebRequestData args, Action<CommandResult> Callback)
    {
        
        if (args is GetCommonItemsRequestData localArgs)
        {
            CommandArgs commandArgs = new GetCommonItemsCommandArgs
            {
                Callback = Callback,
                CatalogVersion = localArgs.CatalogVersion,
                DataStore = localArgs.DataStore
            };

            GetCommonItemsCommand command = CommandFactory
                .Instance
                .CreateCommand<GetCommonItemsCommand>(commandArgs);
            command.Execute();
        }
        else
        {
            // Make more verbose exceptions
            throw new ArgumentException("Incorrect args data type;");
        }
    }

    public void GetStoreItems(WebRequestData args, Action<CommandResult> Callback)
    {
        if (args is GetStoreItemsRequestData localArgs)
        {
            CommandArgs commandArgs = new GetStoreItemsCommandArgs
            {
                Callback = Callback,
                CatalogVersion = localArgs.CatalogVersion,
                StoreId = localArgs.StoreId,
                DataStore = localArgs.DataStore
            };

            GetStoreItemsCommand command = CommandFactory
                .Instance
                .CreateCommand<GetStoreItemsCommand>(commandArgs);
            command.Execute();
        }
        else
        {
            // Make more verbose exceptions
            throw new ArgumentException("Incorrect args data type;");
        }
    }

    public void BuyItem(WebRequestData args, Action<CommandResult> Callback)
    {
        if (args is BuyItemRequestData localArgs)
        {
            CommandArgs commandArgs = new BuyItemCommandArgs
            {
                Callback = Callback,
                ItemID = localArgs.ItemID,
                Price = localArgs.Price,
                CurrencyType = localArgs.CurrencyType
            };

            BuyItemCommand command = CommandFactory
                .Instance
                .CreateCommand<BuyItemCommand>(commandArgs);
            command.Execute();
        }
        else
        {
            // Make more verbose exceptions
            throw new ArgumentException("Incorrect args data type;");
        }
    }

    public void ExecuteServerScript(WebRequestData args, Action<CommandResult> Callback)
    {
        if (args is ExecuteServerScriptRequestData localArgs)
        {
            CommandArgs commandArgs = new ExecuteServerScriptCommandArgs
            {
                Callback = Callback,
                FunctionName = localArgs.FunctionName,
                AnonymousParameter = localArgs.AnonymousParameter,
                ExpectExecutionConfirmation = localArgs.ExpectExecutionConfirmation
            };

            ExecuteServerScriptCommand command = CommandFactory
                .Instance
                .CreateCommand<ExecuteServerScriptCommand>(commandArgs);
            command.Execute();
        }
        else
        {
            // Make more verbose exceptions
            throw new ArgumentException("Incorrect args data type;");
        }
    }

    public void GetPlayerStatistics(WebRequestData args, Action<CommandResult> Callback)
    {
        if (args is GetPlayerStatisticsRequestData localArgs)
        {
            CommandArgs commandArgs = new GetPlayerStatisticsCommandArgs
            {
                Callback = Callback,
                StatisticsNames = localArgs.StatisticsNames
            };

            GetPlayerStatisticsCommand command = CommandFactory
                .Instance
                .CreateCommand<GetPlayerStatisticsCommand>(commandArgs);
            command.Execute();
        }
        else
        {
            // Make more verbose exceptions
            throw new ArgumentException("Incorrect args data type;");
        }
    }

    public void GetLeaderboard(WebRequestData args, Action<CommandResult> Callback)
    {
        if (args is GetLeaderBoardRequestData localArgs)
        {
            CommandArgs commandArgs = new GetLeaderBoardCommandArgs
            {
                Callback = Callback,
                LeaderboardKey = localArgs.LeaderboardKey,
                StartingFrom = localArgs.StartingFrom,
                MaxResultsCount = localArgs.MaxResultsCount
            };

            GetLeaderBoardCommand command = CommandFactory
                .Instance
                .CreateCommand<GetLeaderBoardCommand>(commandArgs);
            command.Execute();
        }
        else
        {
            // Make more verbose exceptions
            throw new ArgumentException("Incorrect args data type;");
        }
    }

    public void MakeTestEntityRequest(WebRequestData args, Action<CommandResult> Callback)
    {
        if (args is EntityTestRequestData localArgs)
        {
            CommandArgs commandArgs = new EntityTestCommandArgs
            {
                Callback = Callback
            };

            EntityTestCommand command = CommandFactory
                .Instance
                .CreateCommand<EntityTestCommand>(commandArgs);
            command.Execute();
        }
        else
        {
            // Make more verbose exceptions
            throw new ArgumentException("Incorrect args data type;");
        }
    }
}
