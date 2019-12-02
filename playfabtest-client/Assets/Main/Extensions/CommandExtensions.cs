using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CommandExtensions
{
    public static void TryOnResult<T>(this CommandResult result, Action<T> onSuccess, Action<string> onError = null)
        where T : CommandResult
    {
        if (result.CommandResultType == CommandResultType.Success)
        {
            if (result is T statisticsResult)
            {
                onSuccess.Invoke(statisticsResult);
            }
            else
            {
                throw new ArgumentException("Expected CommandResult of type " +
                    typeof(GetAccountInfoCommandResult) + " and received of type " +
                    result.GetType());
            }
        }
        else if (result.CommandResultType == CommandResultType.Error)
        {
            if (onError != null)
            {
                onError.Invoke(result.ErrorMessage);
                return;
            }
            else
            {
                throw new Exception(result.ErrorMessage);
            }
        }
        else
        {
            throw new ArgumentException("Enexpected CommandResultType enum: " +
                result.CommandResultType.ToString());
        }
    }
}
