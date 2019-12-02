using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;
using UnityEngine;

public class GetLeaderBoardRequestData : WebRequestData
{
    public string LeaderboardKey { get; set; }
    public int StartingFrom { get; set; }
    public int MaxResultsCount { get; set; }

}

public class GetLeaderBoardCommandArgs : CommandArgs
{
    public string LeaderboardKey { get; set; }
    public int StartingFrom { get; set; }
    public int MaxResultsCount { get; set; }
}


public struct LeaderboardScore
{
    public string playerId;
    public string playerName;
    public int value;
    public int positionOnBoard;
}


public class GetLeaderBoardCommandResult : CommandResult
{
    public SortedList<int, LeaderboardScore> LeaderBoard { get; set; }
        = new SortedList<int, LeaderboardScore>();
}

public class GetLeaderBoardCommand : PlayFabAPICommand
{
    public override void SetExpectedTypes()
    {
        _expectedCommandArgsType = typeof(GetLeaderBoardCommandArgs);
    }

    public override void Execute()
    {
        if (_args is GetLeaderBoardCommandArgs leaderboardArgs)
        {
            var request = new GetLeaderboardRequest
            {
                StatisticName = leaderboardArgs.LeaderboardKey,
                StartPosition = leaderboardArgs.StartingFrom,
                MaxResultsCount = leaderboardArgs.MaxResultsCount
            };

            PlayFabClientAPI.GetLeaderboard(request, OnGotLeaderboard, OnHttpError);
        }
    }

    private void OnGotLeaderboard(GetLeaderboardResult result)
    {
        GetLeaderBoardCommandResult buyItemResult
           = new GetLeaderBoardCommandResult();

        foreach (var score in result.Leaderboard)
        {
            //Debug.Log(score.DisplayName);

            buyItemResult.LeaderBoard.Add(score.Position, new LeaderboardScore
            {
                value = score.StatValue,
                playerId = score.PlayFabId,
                playerName = score.DisplayName,
                positionOnBoard = score.Position
            });
        }

        Callback?.Invoke(buyItemResult);
    }
}
