using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BombPooler))]
public class CannonHandler : MonoBehaviour
{
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private Transform _bombSpawnPoint;
    [SerializeField] private float _shootForce = 15f;

    private BombPooler _bombPooler;
    private GameObject _currentBomb;
    private Rigidbody _currentBombRb;

    public Action EnemyKilled;

    private void Awake()
    {
        _bombPooler = GetComponent<BombPooler>();
        _inputManager.ShootPress += ShootBomb;
    }

    void Start()
    {
        foreach (var bomb in _bombPooler.BombPool)
        {
            bomb
                .GetComponent<Bomb>()
                .BombExploded += (isEnemyKilled) =>
                {
                    if(isEnemyKilled)
                    {
                        EnemyKilled?.Invoke();
                    }
                };
        }
    }

    private void ShootBomb()
    {
        if (_bombPooler.TryGetABomb(out _currentBomb))
        {
            _currentBomb.SetActive(true);
            _currentBomb.transform.position = _bombSpawnPoint.position;
            _currentBomb
                .GetComponent<Rigidbody>()
                .AddForce(_bombSpawnPoint.forward * _shootForce, ForceMode.Impulse);

        }
    }
}
