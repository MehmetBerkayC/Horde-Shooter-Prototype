using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float _speed = 10f;
    [SerializeField] int _damage = 10;
    [SerializeField] float _lifeTime = 3f;

    Transform _enemy;

    private void Start()
    {
        Destroy(this.gameObject, _lifeTime);

        DetectEnemy();
    }

    void Update()
    {
        if (_enemy != null)
        {
            Vector3 direction = _enemy.position - transform.position;
            direction.Normalize();
            transform.Translate(direction * _speed * Time.deltaTime, Space.World);

            transform.forward = _enemy.position - transform.position;
        }
    }

    void DetectEnemy()
    {
        //Physics2D.OverlapCircleAll(transform.position, _range);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out HealthSystem damageableObject) /* Try to check for the entity type*/)
        {
            damageableObject.TakeDamage(_damage);
            Destroy(this.gameObject);
        }
        // For now do not destroy the projectile -> if a monster deploys it, should pass through other monsters
    }
}
