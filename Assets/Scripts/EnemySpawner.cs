using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public enum GameMode
{
    WaveGame,
    EndlessGame
}

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public class Endless
    {
        public List<EnemyGroup> _enemyGroups;
        public int _spawnCount;
    }
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

    public Endless _endless;
    public List<Wave> _waves; //a list of all the waves in the game
    public int _currentWaveCount; // the index of the current wave

    [SerializeField] Transform _player;

    [Header("Spawner Attributes")]
    float _spawnTimer; //timer use to determine when to spawn next enemy
    public int _enemiesAlive;
    public int _maxEnemiesAllowed; //the maximum number of enemies allowed on the map at once
    public bool _maxEnemiesReached; // a flag indicating if the maximum number of enemis has been reached
    public float _waveInterval; //the interval between each wave

    bool _waveCompleted = false;
    public GameMode gameMode = GameMode.WaveGame;
    bool _endlessMode = false;
    float _overallTimer;

    void Start()
    {
        _waveCompleted = true;
        CalculateWaveQuota();

        if (gameMode == GameMode.EndlessGame)
        {
            _endlessMode = true;
        }
        else
        {
            _endlessMode = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (_endlessMode)
        {
            _overallTimer += Time.deltaTime;

        }
        else if (_waveCompleted)
        {
            if (_currentWaveCount < _waves.Count)
            {
                StartCoroutine(StartWave());

                _spawnTimer += Time.deltaTime;

                //check if its time to spawn the next enemy
                if (_spawnTimer >= _waves[_currentWaveCount]._spawnInterval)
                {
                    _spawnTimer = 0f;
                    SpawnEnemies();
                }
            }
            else
            {
                // All waves are completed, you can handle game victory here.
            }
        }

        //if (_currentWaveCount <_waves.Count && _waves[_currentWaveCount]._spawnCount == 0)
        //{
        //    StartCoroutine(BeginNextWave());
        //}  
    }

    IEnumerator StartWave()
    {
        _waveCompleted = false;

        yield return new WaitForSeconds(_waveInterval);

        if (_currentWaveCount < _waves.Count - 1)
        {
            SpawnEnemies();
            _currentWaveCount++;
            CalculateWaveQuota();
        }
    }
    IEnumerator EndlessMode()
    {
        // Wave for '_waveInerval' seconds before starting next wave
        yield return new WaitForSeconds(_overallTimer);

        SpawnEnemies();
    }

    void CalculateWaveQuota()
    {
        int currentWaveQuota = 0;
        foreach (var enemyGroup in _waves[_currentWaveCount]._enemyGroups)
        {
            currentWaveQuota += enemyGroup._enemyCount;
        }
        _waves[_currentWaveCount]._waveQuota = currentWaveQuota;
        //Debug.LogWarning(currentWaveQuota);
    }

    void SpawnEnemiesEndless()
    {
        foreach (var enemyGroup in _endless._enemyGroups)
        {
                Vector2 spawnPosition = new Vector2(_player.transform.position.x + Random.Range(-10f, 10f), _player.transform.position.y + Random.Range(-10f, 10f));
                Instantiate(enemyGroup._enemyPrefab, spawnPosition, Quaternion.identity);

                enemyGroup._spawnCount++;
          
        }
    }

    void SpawnEnemies()
    {
        //check if the minimum number of enemies in the wave have been spawned
        if (_waves[_currentWaveCount]._spawnCount < _waves[_currentWaveCount]._waveQuota && !_maxEnemiesReached)
        {
            //spawn each type of enemies untill the quota is filled
            foreach(var enemyGroup in _waves[_currentWaveCount]._enemyGroups)
            {

                // check if the minumum number of the enemies of this type has been spawned
                if(enemyGroup._spawnCount < enemyGroup._enemyCount)
                {
                    //limit the number of enemies that can be spawned
                    if(_enemiesAlive >=_maxEnemiesAllowed)
                    {
                        _maxEnemiesReached = true;
                        return;
                    }
                    Vector2 spawnPosition = new Vector2(_player.transform.position.x + Random.Range(-10f, 10f), _player.transform.position.y + Random.Range(-10f, 10f));
                    Instantiate(enemyGroup._enemyPrefab, spawnPosition, Quaternion.identity);

                    enemyGroup._spawnCount++;
                    _waves[_currentWaveCount]._spawnCount++;
                    _enemiesAlive++;
                }
            }
        }
        // reset the _maxEnemiesReached flag if the number alive has dropped below the maximum amount
        if(_enemiesAlive< _maxEnemiesAllowed)
        {
            _maxEnemiesReached = false;
        }
    }
    //call this function when enemey is killed
    public void onEnemyKilled()
    {
        //decrement theh number of enemies alive
        _enemiesAlive--;
    }
}
