using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class LeaderboardScoreView : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private List<Color> _topScoresColours = new List<Color>(3);
    [SerializeField] private Color _standardColor;

    [Header("ScoreComponents")]
    [SerializeField] private TextMeshProUGUI _rank;
    [SerializeField] private TextMeshProUGUI _totalXp;
    [SerializeField] private TextMeshProUGUI _playerNick;

    public void SetupLeaderboardScoreView(int rank, int xp, string name)
    {
        _rank.text = (rank+1).ToString();
        _totalXp.text = xp.ToString();
        _playerNick.text = name;

        //Debug.Log(rank + name);

        SetTextColorByRank(rank);
    }

    private void SetTextColorByRank(int rank)
    {
        Color color;

        if(rank < _topScoresColours.Count)
        {
            color = _topScoresColours[rank];
        }
        else
        {
            color = _standardColor;
        }

        _rank.color = color;
        _totalXp.color = color;
        _playerNick.color = color;
    }
}
