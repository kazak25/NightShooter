using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private PlayerHealth _playerHealth;
    [SerializeField] private EnemyController _enemy;
    [SerializeField] private float _spawnTime = 3f;
    [SerializeField] private Transform[] _spawnPoints;


    void Start ()
    {
        InvokeRepeating ("Spawn", _spawnTime, _spawnTime);
    }


    void Spawn ()
    {
        if(_playerHealth.currentHealth <= 0f)
        {
            return;
        }

        int spawnPointIndex = Random.Range (0, _spawnPoints.Length);

      var enemy = Instantiate (_enemy, _spawnPoints[spawnPointIndex].position, _spawnPoints[spawnPointIndex].rotation);
      enemy.Initialize(_playerHealth);
    }
}
