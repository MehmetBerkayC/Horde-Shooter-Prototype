using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Damageable
public class PlayerController : MonoBehaviour, IDamageable
{
    [SerializeField] float _speed = 5f;
    [SerializeField] float _maxHealth = 100f;
    
    [SerializeField] EntityType _entityType = EntityType.Player;
    
    Vector2 _playerInputs;

    Rigidbody2D _rb;

    PlayerAnimator _animator;
    HealthSystem _HealthSystem;

    void Start()
    {
        _HealthSystem = new HealthSystem(_maxHealth, _entityType);
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

    public EntityType GetEntityType()
    {
        return _entityType;
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

    public void TakeDamage(float damageAmount)
    {
        _HealthSystem.TakeDamage(damageAmount);
    }

    public void Heal(float healAmount)
    {
        _HealthSystem.Heal(healAmount);
    }
}
