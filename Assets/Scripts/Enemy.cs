using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] int _maxHealth;
    [SerializeField] float _moveSpeed;
    [SerializeField] float _damage;

    Transform _player;
    HealthSystem _healthSystem;

    void Start()
    {
        _player = FindObjectOfType<Player>().transform;
        
        // Health System
        _healthSystem = new HealthSystem(_maxHealth);
        _healthSystem.OnDead += UnitKilled;
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, _player.transform.position, _moveSpeed * Time.deltaTime);
    }

    public void TakeDamage(int damageAmount)
    {
        _healthSystem.TakeDamage(damageAmount);
    }

    private void UnitKilled(object sender, EventArgs eventArgs)
    {
        _healthSystem.OnDead -= UnitKilled;
        Destroy(this.gameObject);
    }
}
