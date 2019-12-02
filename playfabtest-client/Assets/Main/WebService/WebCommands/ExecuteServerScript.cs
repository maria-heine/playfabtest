using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ExecuteServerScriptRequestData : WebRequestData
{
    public string FunctionName { get; set; }
    public object AnonymousParameter { get; set; }
    public bool ExpectExecutionConfirmation { get; set; }

}

public class ExecuteServerScriptCommandArgs : CommandArgs
{
    public string FunctionName { get; set; }
    public object AnonymousParameter { get; set; }
    public bool ExpectExecutionConfirmation { get; set; }
}

public class ExecuteServerScriptCommandResult : CommandResult
{
    public object FunctionResult { get; set; }
}

public class ExecuteServerScriptCommand : PlayFabAPICommand
{
    public override void SetExpectedTypes()
    {
        _expectedCommandArgsType = typeof(ExecuteServerScriptCommandArgs);
    }

    public override void Execute()
    {
        if (_args is ExecuteServerScriptCommandArgs localArgs)
        {

            var request = new ExecuteCloudScriptRequest
            {
                FunctionName = localArgs.FunctionName,
                FunctionParameter = localArgs.AnonymousParameter,
                GeneratePlayStreamEvent = localArgs.ExpectExecutionConfirmation
            };

            PlayFabClientAPI.ExecuteCloudScript(request, OnExecutedServerScript, OnHttpError);
        }
    }

    private void OnExecutedServerScript(ExecuteCloudScriptResult result)
    {
        ExecuteServerScriptCommandArgs localArgs = _args as ExecuteServerScriptCommandArgs;

        ExecuteServerScriptCommandResult serverScriptResult = 
            new ExecuteServerScriptCommandResult
        {
            // Add more stuff if needed
            FunctionResult = result.FunctionResult
        };

        Callback?.Invoke(serverScriptResult);
    }
}
