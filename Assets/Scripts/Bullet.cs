using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Bullet : MonoBehaviour
{
    [SerializeField] float _speed = 10f;
    [SerializeField] int _damage = 10;
    Transform _enemy;

    private void Start()
    {
        _enemy = FindObjectOfType<EnemyAiMovement>().transform;
    }
    void Update()
    {
        if (_enemy != null)
        {
            Vector3 direction = _enemy.position - transform.position;
            direction.Normalize();
            transform.Translate(direction * _speed * Time.deltaTime, Space.World);
        }
    }

  
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("EnemyAiMovement"))
        { 
            EnemyAiMovement enemy = collision.gameObject.GetComponent<EnemyAiMovement>();

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
