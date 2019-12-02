using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class LobbyStateView : GameStateView
{
    [Header("Lobby loading")]
    [SerializeField] private GameObject _lobbyLoadingScreen;

    [Header("Lobby")]
    [SerializeField] private GameObject _lobbyScreen;
    [SerializeField] private TextMeshProUGUI _playerWelcomeText;
    [SerializeField] private TextMeshProUGUI _playerXpText;

    [Header("Lobby leave buttons")]
    [SerializeField] private Button _openInventoryButton;
    [SerializeField] private Button _openStoreButton;
    [SerializeField] private Button _openLeaderboardButton;
    [SerializeField] private Button _playButton;

    public Action OpenInventoryButtonPressed;
    public Action OpenStoreButtonPressed;
    public Action OpenLeaderboardButtonPressed;
    public Action PlayButtonPressed;

    private void Start()
    {
        _openInventoryButton.onClick.AddListener(() =>
        {
            OpenInventoryButtonPressed.Invoke();
        });

        _openStoreButton.onClick.AddListener(() =>
        {
            OpenStoreButtonPressed.Invoke();
        });

        _playButton.onClick.AddListener(() =>
        {
            PlayButtonPressed.Invoke();
        });

        _openLeaderboardButton.onClick.AddListener(() =>
        {
            OpenLeaderboardButtonPressed.Invoke();
        });

        ToggleLobbyScreen(false);
    }

    public void ToggleLobbyScreen(bool open)
    {
        _lobbyLoadingScreen.SetActive(!open);
        _lobbyScreen.SetActive(open);
    }

    public void SetPlayerWelcomeText(string text)
    {
        _playerWelcomeText.text = text;
    }

    public void SetPlayerXpText(int xp)
    {
        _playerXpText.text = $"xp {xp}";
    }
}
