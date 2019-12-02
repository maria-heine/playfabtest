using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;


public class GetPlayerStatisticsRequestData : WebRequestData
{
    public List<string> StatisticsNames { get; set; }
}

public class GetPlayerStatisticsCommandArgs : CommandArgs
{
    public List<string> StatisticsNames { get; set; }
}

public class GetPlayerStatisticsCommandResult : CommandResult
{
    public Dictionary<string, int> StatisticsDictionary { get; set; }
}

public class GetPlayerStatisticsCommand : PlayFabAPICommand
{
    public override void SetExpectedTypes()
    {
        _expectedCommandArgsType = typeof(GetPlayerStatisticsCommandArgs);
    }

    public override void Execute()
    {
        if (_args is GetPlayerStatisticsCommandArgs getPlayerStatistics)
        {
            var request = new GetPlayerStatisticsRequest
            {
                StatisticNames = getPlayerStatistics.StatisticsNames
            };

            PlayFabClientAPI.GetPlayerStatistics(request, OnGotPlayerStatistics, OnHttpError);
        }
    }

    private void OnGotPlayerStatistics(GetPlayerStatisticsResult result)
    {
        var statisticsDictionary = new Dictionary<string, int>();

        foreach (var stat in result.Statistics)
        {
            statisticsDictionary.Add(stat.StatisticName, stat.Value);
        }

        var getInventoryResult = new GetPlayerStatisticsCommandResult
        {
            StatisticsDictionary = statisticsDictionary
        };

        Callback?.Invoke(getInventoryResult);
    }
}
