using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private float _autoExplodeTime = 3f;

    private Rigidbody _rb;
    private float _currentLifeTime;

    public Action<bool> BombExploded;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void OnDisable()
    {
        _currentLifeTime = 0f;
        _rb.velocity = Vector3.zero;
    }

    private void Update()
    {
        _currentLifeTime += Time.deltaTime;

        if(_currentLifeTime >= _autoExplodeTime)
        {
            ExplodeBomb(false);
        }
    }

    public void ExplodeBomb(bool isEnemyKilled)
    {
        BombExploded?.Invoke(isEnemyKilled);
        gameObject.SetActive(false);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.gameObject.tag == "Enemy")
        {
            ExplodeBomb(true);
        }
        else
        {
            ExplodeBomb(false);
        }
    }
}
