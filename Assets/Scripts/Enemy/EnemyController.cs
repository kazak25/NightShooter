﻿using System;
using UnityEngine;
using System.Collections;
using Events;
using SimpleEventBus.Disposables;
using UnityEngine.Events;

public class EnemyController : MonoBehaviour
{
    public float TimeBetweenAttacks = 0.5f;
    public int attackDamage = 10;

    [SerializeField] private EnemyHealthController _enemyHealthController;
    [SerializeField] private EnemyMovement _enemyMovement;

    private PlayerHealth _playerHealth;
    private bool _playerInRange;
    private float _timer;
    private CompositeDisposable _subscription;


    public void Initialize(PlayerHealth playerHealth)
    {
        _playerHealth = playerHealth;
        _enemyMovement.Initialize(playerHealth);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _playerInRange = true;
        }
    }


    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _playerInRange = false;
        }
    }


    void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= TimeBetweenAttacks && _playerInRange && _enemyHealthController.CurrentHealth > 0)
        {
            Attack();
        }

        if (_playerHealth.currentHealth <= 0)
        {
            var eventDataRequest = new GetPlayerDeadEvent();
            EventStream.Game.Publish(eventDataRequest);
        }
    }

    void Attack()
    {
        _timer = 0f;

        if (_playerHealth.currentHealth > 0)
        {
            _playerHealth.TakeDamage(attackDamage);
        }
    }

    private void OnDestroy()
    {
        _subscription?.Dispose();
    }
}