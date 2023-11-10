using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public enum GameMode
{
    WaveGame,
    EndlessGame
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

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject[] _enemyPrefabs;
    [SerializeField] Transform _player;
    [SerializeField] float _endlessRate;

    public event EventHandler OnWavePassed;
    //public Endless _endless;
    public List<Wave> _waves; //a list of all the waves in the game
    public int _currentWaveCount; // the index of the current wave


    [Header("Spawner Attributes")]
    float _spawnTimer; //timer use to determine when to spawn next enemy
    public int _enemiesAlive;
    public int _maxEnemiesAllowed; //the maximum number of enemies allowed on the map at once
    public bool _maxEnemiesReached; // a flag indicating if the maximum number of enemis has been reached
    public float _waveInterval; //the interval between each wave
    float _enemiesSpawnedInWave;

    bool _waveCompleted;
    public GameMode gameMode;
    bool _endlessMode = false;

    public static EnemySpawner Instance;

    Vector2 _spawnLocation;

    public float timeInterval = 10.0f; // The time interval you want to restrict the event to
    private float lastEventTime = 0.5f;
    private bool canInvoke = true;

    private bool _waveEndSignal = true;

    void Start()
    {
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

    private void Awake()
    {
        // Singleton Pattern
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (_endlessMode)
        {
            //StartCoroutine(EndlessWave());
            _spawnTimer += Time.deltaTime;

            //check if its time to spawn the next enemy
            if (_spawnTimer >= _endlessRate)
            {
                _spawnTimer = 0f;
                SpawnEndless();
            }
        }
        else
        {
            if (_currentWaveCount < _waves.Count)
            {
                // Spawning Mobs
                _spawnTimer += Time.deltaTime;

                //check if its time to spawn the next enemy
                if (_spawnTimer >= _waves[_currentWaveCount]._spawnInterval)
                {
                    _spawnTimer = 0f;
                    SpawnEnemies();
                }

                // Wave End Check
                if (_enemiesAlive == 0 && _waves[_currentWaveCount]._waveQuota == _enemiesSpawnedInWave && canInvoke)
                {
                    canInvoke = false;
                    OnWavePassed?.Invoke(this, EventArgs.Empty);
                    StartCoroutine(StartWave());
                }

            }
            else
            {
                // All waves are completed, you can handle game victory here.
                // Probably an event to GameManager
            }
        }
    }

    IEnumerator StartWave()
    {
        yield return new WaitForSeconds(_waveInterval);

        if (_currentWaveCount < _waves.Count - 1)
        {
            canInvoke = true;
            SpawnEnemies();
            _currentWaveCount++;
            CalculateWaveQuota();
        }
    }

    //IEnumerator EndlessWave()
    //{
    //    yield return new WaitForSeconds(_endlessRate);
    //    Debug.Log("ben dogdum");
    //    SpawnEndless();

    //}
    void SpawnEndless()
    {
        int rand = UnityEngine.Random.Range(0, _enemyPrefabs.Length);
        GameObject enemyToSpawn = _enemyPrefabs[rand];
        _spawnLocation = new Vector2(UnityEngine.Random.Range(-9.5f, 9.5f), UnityEngine.Random.Range(-9.5f, 9.5f));
        Instantiate(enemyToSpawn, _spawnLocation, Quaternion.identity);
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


    void SpawnEnemies()
    {
        //check if the minimum number of enemies in the wave have been spawned
        if (_waves[_currentWaveCount]._spawnCount < _waves[_currentWaveCount]._waveQuota && !_maxEnemiesReached)
        {
            //spawn each type of enemies untill the quota is filled
            foreach (var enemyGroup in _waves[_currentWaveCount]._enemyGroups)
            {

                // check if the minumum number of the enemies of this type has been spawned
                if (enemyGroup._spawnCount < enemyGroup._enemyCount)
                {
                    //limit the number of enemies that can be spawned
                    if (_enemiesAlive >= _maxEnemiesAllowed)
                    {
                        _maxEnemiesReached = true;
                        return;
                    }
                    //_spawnLocation = new Vector2(Random.Range(-9.5f, 9.5f), Random.Range(-9.5f, 9.5f));
                    Vector2 _spawnLocation = CalculateSpawnPosition();
                    Instantiate(enemyGroup._enemyPrefab, _spawnLocation, Quaternion.identity);

                    enemyGroup._spawnCount++;
                    _waves[_currentWaveCount]._spawnCount++;
                    _enemiesAlive++;
                    _enemiesSpawnedInWave++;
                }
            }
        }
        // reset the _maxEnemiesReached flag if the number alive has dropped below the maximum amount
        if (_enemiesAlive < _maxEnemiesAllowed)
        {
            _maxEnemiesReached = false;
        }
    }

    private Vector2 CalculateSpawnPosition()
    {
        float mapBorder = 9.5f; // Adjust this value based on your map size.
        float minDistanceFromPlayer = 3.0f; // Minimum distance from the player.

        Vector2 spawnPosition;
        do
        {
            float randomX = UnityEngine.Random.Range(-mapBorder, mapBorder);
            float randomY = UnityEngine.Random.Range(-mapBorder, mapBorder);

            spawnPosition = new Vector2(randomX, randomY);
        } while (Vector2.Distance(spawnPosition, _player.position) < minDistanceFromPlayer);

        return spawnPosition;
    }

    //call this function when enemey is killed
    public void onEnemyKilled()
    {
        //decrement theh number of enemies alive
        _enemiesAlive--;
    }
}