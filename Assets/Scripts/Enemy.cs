using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : HealthSystem
{
    [SerializeField] EnemyDataSO _enemyData;
     float _moveSpeed;
     float _damage;

    Transform _player;
    
    void Start()
    {
        _player = FindObjectOfType<Player>().transform;
        InitEnemy();
        EnemySpawner.Instance.OnWavePassed += EnemyUnitUpgrade;
    }

    private void InitEnemy()
    {
        if (_enemyData != null)
        {
            GetComponent<SpriteRenderer>().sprite = _enemyData.sprite;
            _moveSpeed = _enemyData.Speed;
            _damage = _enemyData.Damage;
            MaxHealth = _enemyData.Health;
        }
    }
    public void EnemyUnitUpgrade(object sender, System.EventArgs e)
    {
        Debug.Log("Wave Pass");
        _damage += 2f;
        _moveSpeed += 1f;
        MaxHealth += 5f;         
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, _player.transform.position, _moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out IDamageable enemy))
        {
            //Debug.Log("Hit detected, from:" + gameObject.name + " to:" + enemy.gameObject.name);
            enemy.TakeDamage(_damage);
        }
    }
}
