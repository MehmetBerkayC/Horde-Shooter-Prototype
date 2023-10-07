using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : HealthSystem
{
    [SerializeField] float _speed = 5f;
    
    [SerializeField] UI_Inventory _uiInventory;

    Vector2 _playerInputs;

    Rigidbody2D _rigidbody;
    PlayerAnimator _animator;
    Inventory _playerInventory;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<PlayerAnimator>();
        
        _playerInventory = new Inventory();
        _uiInventory.SetInventory(_playerInventory);
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
        _rigidbody.velocity = _playerInputs * _speed;
        _animator.Flip(_playerInputs.x);
    }
}
