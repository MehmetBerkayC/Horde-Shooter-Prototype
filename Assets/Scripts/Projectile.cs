using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float _speed = 10f;
    [SerializeField] int _damage = 10;
    [SerializeField] float _lifeTime = 3f;

    Transform _enemy;
    EntityType _projectileFromEntity;

    public void Initialize(EntityType entityType)
    {
        _projectileFromEntity = entityType;
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
        if (collision.gameObject.TryGetComponent(out HealthSystem damageableObject) /* Try to check for the entity type*/)
        {
            if(damageableObject.Entity != _projectileFromEntity)
            {
                damageableObject.TakeDamage(_damage);
                Destroy(this.gameObject);
            }
        }
        // For now do not destroy the projectile -> if a monster deploys it, should pass through other monsters
    }
}
