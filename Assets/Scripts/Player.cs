using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[
    RequireComponent(typeof(Rigidbody2D)), 
    RequireComponent(typeof(PlayerAnimator))
]
public class Player : MonoBehaviour, IDamageable
{
    [SerializeField] int _maxHealth = 100;
    [SerializeField] float _speed = 5f;

    // UI
    [SerializeField] Camera _uiCamera;
    [SerializeField] UI_Inventory _uiInventory;
    [SerializeField] InventoryScriptableObject _inventory;
    [SerializeField] UI_LevelBar _uiLevelBar;
    
    // Health
    HealthSystem _healthSystem;
    
    // Level
    LevelSystem _levelSystem;
    LevelSystemAnimated _levelSystemAnimated;

    Vector2 _playerInputs;

    Rigidbody2D _rigidbody;
    PlayerAnimator _animator;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<PlayerAnimator>();

        // Health System
        _healthSystem = new HealthSystem(_maxHealth);
        _healthSystem.OnDead += UnitKilled;

        // Level System -> Level system should be in GameManager, player only interacts with animated level system
        // Make a SetLevelSystem function when that happens and sub to the animated events
        _levelSystem = new LevelSystem();
        _levelSystemAnimated = new LevelSystemAnimated(_levelSystem);
       
    }

    void Start()
    {
        //_inventory = new Inventory(); // may want to return to basic class
        if (_inventory == null)
        {
            Debug.LogError("Player is missing its inventory");
        }

        // Level Bar Initialize -> Should be in GameManager.cs
        _uiLevelBar.SetLevelSystem(_levelSystem);
        _uiLevelBar.SetLevelSystemAnimated(_levelSystemAnimated);
    }

    void Update()
    {
        InputManagement();
    }

    void FixedUpdate()
    {
        Movement();
    }
    
    void InputManagement()
    {
        _playerInputs = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (Input.GetKeyDown(KeyCode.K))
        {
            _inventory.Save();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            _inventory.Load();
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        { 
            ToggleInventoryView();
        }

        // Placeholder Experience
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _levelSystem.AddExperience(20);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _levelSystem.AddExperience(200);
        }
    }

    public Vector2 GetPlayerInputs()
    {
        return _playerInputs;
    }

    private void ToggleInventoryView()
    {
        _uiInventory.gameObject.SetActive(!_uiInventory.isActiveAndEnabled);
    }

    void Movement()
    {
        _rigidbody.velocity = _playerInputs * _speed;
        _animator.Flip(_playerInputs.x);
    }

    public void TakeDamage(int damageAmount)
    {
        _healthSystem.TakeDamage(damageAmount);
    }

    private void UnitKilled(object sender, EventArgs e)
    {
        _healthSystem.OnDead -= UnitKilled;
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out ItemWorld item))
        {
            _inventory.AddItem(new Item(item.GetItemData), item.GetItemData.Amount);
            item.DestroySelf();
        }
    }

    private void OnApplicationQuit()
    {
        _inventory.Container.Clear();
    }
}
