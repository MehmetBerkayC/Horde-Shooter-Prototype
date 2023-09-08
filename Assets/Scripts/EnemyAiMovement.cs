using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAiMovement : MonoBehaviour
{
    [SerializeField] float _moveSpeed;
    [SerializeField] int _maxHealth = 100;
    int _currentHealth;

    Transform _player;
    
    void Start()
    {
        _currentHealth = _maxHealth;
        _player = FindObjectOfType<PlayerMovement>().transform;
    }

    
    void Update()
    {
        // Debug.Log("enemy healt is" +  currentHealth);
        transform.position = Vector2.MoveTowards(transform.position, _player.transform.position, _moveSpeed * Time.deltaTime);
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage; 

        if (_currentHealth <= 0)
        {
            Die(); 
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
