using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardStateModel : GameStateModel
{
    public SortedList<int, LeaderboardScore> LeaderBoard { get; set; }

    public void SetLeaderBoard(SortedList<int, LeaderboardScore> board)
    {
        LeaderBoard = board;
    }

    public void QueryLeaderboard(Action<LeaderboardScore> query)
    {
        foreach(KeyValuePair<int, LeaderboardScore> score in LeaderBoard)
        {
            query.Invoke(score.Value);
        }
    }
}
