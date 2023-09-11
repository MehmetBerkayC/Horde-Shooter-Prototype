using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float _speed = 10f;
    [SerializeField] int _damage = 10;
    Transform _enemy;

    private void Start()
    {
        _enemy = FindObjectOfType<Enemy>().transform;
    }
    void Update()
    {
        if (_enemy != null)
        {
            Vector3 direction = _enemy.position - transform.position;
            direction.Normalize();
            transform.Translate(direction * _speed * Time.deltaTime, Space.World);
            transform.LookAt(_enemy);
        }
    }

  
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("EnemyAiMovement"))
        { 
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();

            if (enemy != null)
            {
                enemy.TakeDamage(_damage);
            }
            Destroy(gameObject);
        }
        else
        { 
            Destroy(gameObject);
        }
    }
}
