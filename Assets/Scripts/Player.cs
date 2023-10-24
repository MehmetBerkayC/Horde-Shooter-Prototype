using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[
    RequireComponent(typeof(Rigidbody2D)), 
    RequireComponent(typeof(PlayerAnimator))
]
public class Player : HealthSystem
{
    [SerializeField] float _speed = 5f;
    
    [SerializeField] UI_Inventory _uiInventory;
    [SerializeField] InventorySObject _inventory;

    Vector2 _playerInputs;

    Rigidbody2D _rigidbody;
    PlayerAnimator _animator;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<PlayerAnimator>();
    }

    void Start()
    {
        //_inventory = new Inventory();
        if (_inventory == null)
        {
            Debug.LogError("Player is missing its inventory");
        }
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out ItemWorld item))
        {
            _inventory.AddItem(item.GetItem(),1);
            item.DestroySelf();
        }
    }

    private void OnApplicationQuit()
    {
        _inventory.Container.Clear();
    }
}
