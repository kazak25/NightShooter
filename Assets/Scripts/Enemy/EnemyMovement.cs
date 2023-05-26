using System;
using UnityEngine;
using System.Collections;
using Events;
using SimpleEventBus.Disposables;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private EnemyHealthController _enemyHealthController;
    [SerializeField] private UnityEngine.AI.NavMeshAgent _nav;
   
    
    private Transform _playerPoint;
    private PlayerHealth _playerHealth;
    private CompositeDisposable _subscription;
    

    public void Initialize(PlayerHealth playerHealth)
    {
        _playerHealth = playerHealth;
        _playerPoint = _playerHealth.transform;
    }
    
    void Update()
    {
        if (_enemyHealthController.CurrentHealth > 0 && _playerHealth.currentHealth > 0)
        {
            _nav.SetDestination(_playerPoint.position);
        }
        else
        {
            _nav.enabled = false;
        }
    }

    private void OnDestroy()
    {
        _subscription?.Dispose();
    }
}