using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float _speed = 5f;

    Vector2 _playerInputs;

    Rigidbody2D _rb;

    PlayerAnimator _animator;


    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<PlayerAnimator>();
    }

    
    void Update()
    {
        InputManagement();
    }

    void FixedUpdate()
    {
        Movement();
    }

    public Vector2 GetPlayerInputs()
    {
        return _playerInputs;
    }
    
    void InputManagement()
    {
        _playerInputs = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

    }

    void Movement()
    {
        _rb.velocity = _playerInputs * _speed;
        _animator.Flip(_playerInputs.x);
    }
}
