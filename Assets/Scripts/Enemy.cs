using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : HealthSystem
{
    [SerializeField] float _moveSpeed;
    [SerializeField] float _damage;

    Transform _player;
    
    void Start()
    {
        _player = FindObjectOfType<PlayerController>().transform;
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
