using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float _moveSpeed;
    [SerializeField] int _maxHealth = 100;

    Transform _player;
    HealthSystem _health;
    
    void Start()
    {
        _health = new HealthSystem(_maxHealth);
        _player = FindObjectOfType<PlayerController>().transform;
    }


    void Update()
    {
        // Debug.Log("enemy healt is" +  currentHealth);
        transform.position = Vector2.MoveTowards(transform.position, _player.transform.position, _moveSpeed * Time.deltaTime);
    }

}
