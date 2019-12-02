using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private Vector2 _spawnBounds;

    private const float SPAWN_TIME = 1.5f;
    private int _enemyPoolCount = 10;
    private float _currentTime;
    private GameObject _currentSpawn;

    public List<GameObject> EnemyPool { get; private set; }

    private void Awake()
    {
        LoadEnemyPool();
    }

    private void Update()
    {
        if(_currentTime >= SPAWN_TIME)
        {
            _currentTime = 0f;
            TrySpawnEnemy();
        }

        _currentTime += Time.deltaTime;
    }

    private void TrySpawnEnemy()
    {
        if(TryGetAnEnemy(out _currentSpawn))
        {
            _currentSpawn.SetActive(true);
            float x = Mathf.Lerp(_spawnBounds.x, _spawnBounds.y, GetARandomFloat());
            float z = Mathf.Lerp(_spawnBounds.x, _spawnBounds.y, GetARandomFloat());
            _currentSpawn.transform.position = new Vector3(x, 1f, z);
        }
    }

    private float GetARandomFloat()
    {
        return Random.Range(0f, 1f);
    }

    private void LoadEnemyPool()
    {
        EnemyPool = new List<GameObject>(_enemyPoolCount);
        for (int i = 0; i < _enemyPoolCount; i++)
        {
            GameObject enemy = (GameObject)Instantiate(_enemyPrefab, transform);
            enemy.SetActive(false);
            EnemyPool.Add(enemy);
        }
    }

    private bool TryGetAnEnemy(out GameObject enemy)
    {
        for (int i = 0; i < EnemyPool.Count; i++)
        {
            if (!EnemyPool[i].activeInHierarchy)
            {
                enemy = EnemyPool[i];
                return true;
            }
        }

        enemy = null;

        return false;
    }
}
