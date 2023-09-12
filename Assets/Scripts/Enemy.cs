using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] EntityType _entityType;
    [SerializeField] float _moveSpeed;
    [SerializeField] int _maxHealth = 100;

    Transform _player;
    HealthSystem _HealthSystem;
    
    void Start()
    {
        _HealthSystem = new HealthSystem(_maxHealth, _entityType);

        _player = FindObjectOfType<PlayerController>().transform;
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, _player.transform.position, _moveSpeed * Time.deltaTime);
    }

    public void TakeDamage(float damageAmount)
    {
        _HealthSystem.TakeDamage(damageAmount);
    }
}
