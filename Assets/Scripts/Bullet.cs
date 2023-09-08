using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 10;
    private Transform enemy;

    private void Start()
    {
        enemy = FindObjectOfType<EnemyAiMovement>().transform;
    }
    void Update()
    {
        if (enemy != null)
        {
            
            Vector3 direction = enemy.position - transform.position;
            direction.Normalize();
            transform.Translate(direction * speed * Time.deltaTime, Space.World);
        }
       

    }

  
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("EnemyAiMovement"))
        { 
            EnemyAiMovement enemy = collision.gameObject.GetComponent<EnemyAiMovement>();

            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
        else
        { 
            Destroy(gameObject);
        }
    }
}
