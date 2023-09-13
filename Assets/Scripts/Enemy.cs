using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthSystem))]
public class Enemy : MonoBehaviour
{
    [SerializeField] float _moveSpeed;

    Transform _player;
    HealthSystem _HealthSystem;
    
    void Start()
    {
        _player = FindObjectOfType<PlayerController>().transform;
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, _player.transform.position, _moveSpeed * Time.deltaTime);
    }

}
