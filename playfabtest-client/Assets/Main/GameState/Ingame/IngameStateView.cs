using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

// Require reworking, the prefab shouldnt contain game logic
public class IngameStateView : GameStateView
{
    [Header("Ingame UI")]
    [SerializeField] private GameObject _ingameUIScreen;
    [SerializeField] private TextMeshProUGUI _gameTimeDisplayer;
    [SerializeField] private TextMeshProUGUI _killCountDisplayer;

    [Header("Endgame UI")]
    [SerializeField] private GameObject _endgameScreen;
    [SerializeField] private TextMeshProUGUI _endgameScoreDisplayer;
    [SerializeField] private Button _endGameButton;

    // Probably better separate concerns
    [Header("Gameplay")]
    [SerializeField] private GameObject _playerCube;
    [SerializeField] private CannonHandler _cannonHandler;

    private GameObject _menuCamera;
    private float _activeGameTime = 0f;

    public Action PlayerKilledAnEnemy;
    public Action<float> OnGameTimeUpdated;
    public Action EndGameButtonPressed;

    void Start()
    {
        ToggleMenuCamera(false);
        TogglePlayerCursor(false);
        ToggleEndGameButton(false);

        _ingameUIScreen.SetActive(true);
        _endgameScreen.SetActive(false);

        transform.position = Vector3.zero;

        _cannonHandler.EnemyKilled += OnEnemyKilled;

        _endGameButton.onClick.AddListener(() =>
        {
            EndGameButtonPressed.Invoke();
        });
    }

    private void Update()
    {
        _activeGameTime += Time.deltaTime;

        OnGameTimeUpdated?.Invoke(_activeGameTime);
    }

    private void OnDestroy()
    {
        ToggleMenuCamera(true);
        TogglePlayerCursor(true);
    }

    #region PRIVATE

    private void TogglePlayerCursor(bool toggle)
    {
        if (toggle)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    private void ToggleMenuCamera(bool toggle)
    {
        if (_menuCamera == null)
        {
            _menuCamera = GameObject.FindGameObjectWithTag("MainCamera");
        }

        _menuCamera.SetActive(toggle);
    }

    private void OnEnemyKilled()
    {
        PlayerKilledAnEnemy.Invoke();
    }


    #endregion

    #region PUBLIC
    public void UpdateGameTimeDisplayer(float timeLeft)
    {
        if (timeLeft < 5f) _gameTimeDisplayer.color = Color.red;
        _gameTimeDisplayer.text = $"{timeLeft} seconds left";
    }

    public void UpdateKillCounter(int killCount)
    {
        _killCountDisplayer.text = $"kILLED {killCount} eNEMIES";
    }

    public void DisplayEndScreen(int score)
    {
        _ingameUIScreen.SetActive(false);
        _endgameScreen.SetActive(true);
        TogglePlayerCursor(true);
        _cannonHandler.EnemyKilled -= OnEnemyKilled;
        _endgameScoreDisplayer.text = $"You destroyed {score} enemies.";
    }

    public void TogglePlayerCube(bool toggle)
    {
        // hackyyyyyy
        _playerCube.GetComponent<InputManager>().enabled = toggle;
    }

    public void ToggleEndGameButton(bool toggle)
    {
        _endGameButton.interactable = toggle;
    }

    #endregion

}
