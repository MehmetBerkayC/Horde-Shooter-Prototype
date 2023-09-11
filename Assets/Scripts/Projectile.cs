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
            transform.right = _enemy.position - transform.position;
        }
    }

  
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        { 
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();

            if (enemy != null)
            {
            }
            Destroy(gameObject);
        }
        else
        { 
            Destroy(gameObject);
        }
    }
}
