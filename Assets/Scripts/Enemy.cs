using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    // Work:
    // Take base info from SO, make it functional with upgradability

    // Missing Pathing using NavMesh or Custom
    [SerializeField] EnemyDataSO _enemyData;

    float _baseDamage;
    int _baseHealth;
    int _baseExperience;
    float _baseMovementSpeed;

    Transform _player;
    HealthSystem _healthSystem;
    SpriteRenderer _spriteRenderer;

    void Start()
    {
        // Player Transform for tracking
        _player = Player.Instance.transform;

        InitializeEnemy();
    }

    void InitializeEnemy() // Call in start
    {   
        // Base Stats
        _baseDamage = _enemyData.BaseDamage;
        _baseHealth = _enemyData.BaseHealth;
        _baseExperience     = _enemyData.BaseExperience;
        _baseMovementSpeed  = _enemyData.BaseMovementSpeed;

        // Health System
        _healthSystem = new HealthSystem(_baseHealth);
        _healthSystem.OnDead += UnitKilled;

        // Default Sprites
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = _enemyData.Sprite;
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, _player.transform.position, _baseMovementSpeed * Time.deltaTime);
    }

    public void TakeDamage(int damageAmount)
    {
        _healthSystem.TakeDamage(damageAmount);
    }

    private void UnitKilled(object sender, EventArgs eventArgs)
    {
        // Destroy doesn't destroy gameobject until frame ends
        Destroy(this.gameObject);
        
        // Add Exp to player
        GameManager.Instance.PlayerLevelSystem.AddExperience(_baseExperience);
        
        // Unsub from HealthSystem
        _healthSystem.OnDead -= UnitKilled;
        
        // Flag EnemySpawner for the kill
        EnemySpawner.Instance.EnemyKilled();
    }
}
