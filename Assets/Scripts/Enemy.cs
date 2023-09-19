using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : HealthSystem
{
    [SerializeField] float _moveSpeed;
    [SerializeField] float _damage;

    Transform _player;
    HealthSystem _HealthSystem;

    
    void Start()
    {
        _player = FindObjectOfType<PlayerController>().transform;
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, _player.transform.position, _moveSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out HealthSystem enemyHealthSystem))
        {
            if (_HealthSystem.Entity != enemyHealthSystem.Entity)
            {
                enemyHealthSystem.TakeDamage(_damage);
            }
        }
    }
}
