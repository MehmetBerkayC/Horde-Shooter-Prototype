using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    float
        _speed = 500,
        _lifeTime = 3;

    [SerializeField] int _damage;
     
    Transform _enemy;
    Rigidbody2D _rigidbody;

    private void Start()
    {
        Destroy(this.gameObject, _lifeTime);
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void SetEnemy(Transform enemy)
    {
        _enemy = enemy;
    }

    void FixedUpdate()
    {
        if (_enemy != null)
        {
            Vector2 direction = (_enemy.position - transform.position).normalized;
            _rigidbody.velocity = direction * _speed * Time.deltaTime * 10f;
        }
    }
    public void SetDamage(int damage)
    {
        _damage = damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IDamageable enemy))
        {
            Debug.Log("Projectile hit to:" + enemy);
            enemy.TakeDamage(_damage);
            Destroy(this.gameObject);
        }
    }
}
