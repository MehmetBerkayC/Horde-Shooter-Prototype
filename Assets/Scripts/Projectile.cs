using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float _speed = 10f;
    [SerializeField] int _damage = 10;
    [SerializeField] float _lifeTime = 3f;

    Transform _enemy;
    Entity _projectileFromEntity;

    public void Initialize(Entity entity)
    {
        _projectileFromEntity = entity;
    }

    private void Start()
    {
        Destroy(this.gameObject, _lifeTime);
        _enemy = FindObjectOfType<Enemy>().transform;
    }
    void Update()
    {
        if (_enemy != null)
        {
            Vector3 direction = _enemy.position - transform.position;
            direction.Normalize();
            transform.Translate(direction * _speed * Time.deltaTime, Space.World);

            transform.right = _enemy.position - transform.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>().GetEntityType() != _projectileFromEntity)
        {
            var enemy = collision.gameObject.GetComponent<HealthSystem>();

            if (enemy != null)
            {
                enemy.TakeDamage(_damage);
            }
        }
        Destroy(this.gameObject);
    }
}
