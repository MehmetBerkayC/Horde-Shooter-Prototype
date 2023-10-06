using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public string _waveName;
        public List<EnemyGroup> _enemyGroups; // a list of groups of enemies to spawn in this wave
        public int _waveQuota; // total number of the enemies ti spawn in this wave
        public float _spawnInterval; //the interval at which to spawn enemies
        public int _spawnCount; //the number of enemies already spawned in this wave
    }

    [System.Serializable]
    public class EnemyGroup
    {
        public string _enemyName;
        public int _enemyCount; // the number of enemies to spawn in this wave
        public float _spawnCount; // the number of enemies of this type already spawned in this wave
        public GameObject _enemyPrefab;
    }

    public List<Wave> _waves; //a list of all the waves in the game
    public int _currentWaveCount; // the index of the current wave

    Transform _player;

    [Header("Spawner Attributes")]
    float _spawnTimer; //timer use to determine when to spawn next enemy

    void Start()
    {
        _player = FindObjectOfType<PlayerController>().transform; // Aþýrý yavaþ, inspectorden manuel ata
        CalculateWaveQuota();
        SpawnEnemies();
    }

    // Update is called once per frame
    void Update()
    {
        _spawnTimer += Time.deltaTime;

        //check if its time to spawn the next enemy
        if(_spawnTimer >= _waves[_currentWaveCount]._spawnInterval)
        {
            _spawnTimer = 0f;
            SpawnEnemies();
        }
    }

    void CalculateWaveQuota()
    {
        int _currentWaveQuota = 0;
        foreach (var enemyGroup in _waves[_currentWaveCount]._enemyGroups)
        {
            _currentWaveQuota += enemyGroup._enemyCount;
        }
        _waves[_currentWaveCount]._waveQuota = _currentWaveQuota;
        Debug.LogWarning(_currentWaveQuota);
    }

    void SpawnEnemies()
    {
        //check if the minimum number of enemies in the wave have been spawned
        if (_waves[_currentWaveCount]._spawnCount < _waves[_currentWaveCount]._waveQuota)
        {
            //spawn each type of enemies untill the quota is filled
            foreach(var enemyGroup in _waves[_currentWaveCount]._enemyGroups)
            {
                // check if the minumum number of the enemies of this type has been spawned
                if(enemyGroup._spawnCount < enemyGroup._enemyCount)
                {
                    Vector2 spawnPosition = new Vector2(_player.transform.position.x + Random.Range(-10f, 10f), _player.transform.position.y + Random.Range(-10f, 10f));
                    Instantiate(enemyGroup._enemyPrefab, spawnPosition, Quaternion.identity);

                    enemyGroup._enemyCount++;
                    _waves[_currentWaveCount]._spawnCount++;
                }
            }
        }
    }
}
