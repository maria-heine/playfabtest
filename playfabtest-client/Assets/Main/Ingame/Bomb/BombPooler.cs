using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombPooler : MonoBehaviour
{
    [SerializeField] private GameObject _bombPrefab;
    [SerializeField] private int _poolCount;

    public List<GameObject> BombPool { get; private set; }

    private void Awake()
    {
        BombPool = new List<GameObject>(_poolCount);
        for (int i = 0; i < _poolCount; i++)
        {
            GameObject bomb = (GameObject)Instantiate(_bombPrefab);
            bomb.SetActive(false);
            BombPool.Add(bomb);
        }
    }

    public bool TryGetABomb(out GameObject bomb)
    {
        for (int i = 0; i < BombPool.Count; i++)
        {
            if (!BombPool[i].activeInHierarchy)
            {
                bomb = BombPool[i];
                return true;
            }
        }

        bomb = null;

        return false;
    }
}
