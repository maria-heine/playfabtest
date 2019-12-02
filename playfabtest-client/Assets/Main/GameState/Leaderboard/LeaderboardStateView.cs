using System;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardStateView : GameStateView
{
    [Header("Loading")]
    [SerializeField] private GameObject _leaderboardLoadingScreen;

    [Header("Leaderboard Screen")]
    [SerializeField] private GameObject _leaderboardScreen;
    [SerializeField] private VerticalLayoutGroup _leaderboardLayout;
    [SerializeField] private Button _goBackButton;

    [Header("Leaderboard")]
    [SerializeField] private GameObject _leaderboardScorePrefab;

    public Action GoBackButtonPressed;

    public void Start()
    {
        _leaderboardLoadingScreen.SetActive(true);
        _leaderboardScreen.SetActive(false);
        _leaderboardLayout.Clear();
        _goBackButton.onClick.AddListener(() =>
        {
            GoBackButtonPressed?.Invoke();
        });
    }

    public void LoadLeaderboardScore(LeaderboardScore score)
    {
        var scorePrefabView = GameObject
            .Instantiate(_leaderboardScorePrefab, _leaderboardLayout.transform, false)
            .GetComponent<LeaderboardScoreView>();

        string name = score.playerName != null ? score.playerName : score.playerId;

        scorePrefabView
            .SetupLeaderboardScoreView(score.positionOnBoard, score.value, name);
    }

    public void DisplayLeaderboard()
    {
        _leaderboardLoadingScreen.SetActive(false);
        _leaderboardScreen.SetActive(true);
    }
}
