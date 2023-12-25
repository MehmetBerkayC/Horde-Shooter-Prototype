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
    public static Player Instance { get; private set; }

    // This will become a SO based upgradable stat system later
    [SerializeField] int _maxHealth = 100;
    [SerializeField] float _speed = 5f;

    // UI
    [SerializeField] Camera _uiCamera;
    [SerializeField] UI_Inventory _uiInventory;
    [SerializeField] InventoryScriptableObject _inventory;
    [SerializeField] UI_LevelBar _uiLevelBar;
    
    // Health
    HealthSystem _healthSystem;

    Vector2 _playerInputs;

    Rigidbody2D _rigidbody;
    PlayerAnimator _animator;

    // Gun Slots
    [SerializeField] Transform[] _availableGunSlots; // May automatically search using transform.find later
    [SerializeField] GunLoadoutDataSO _gunLoadoutData;
    GunLoadout _gunLoadout;

    void Awake()
    { 
        // Singleton Pattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<PlayerAnimator>();

    }

    void Start()
    {
        //_inventory = new Inventory(); // may want to return to basic class
        if (_inventory == null)
        {
            Debug.LogError("Player is missing its inventory");
        }

        // Health System
        _healthSystem = new HealthSystem(_maxHealth);
        _healthSystem.OnDead += UnitKilled;

        // Gun Loadout
        if (_gunLoadoutData != null) // decide what to do if loadout not assigned
        {
            _gunLoadout = new GunLoadout(_gunLoadoutData, _availableGunSlots); // First time loadout equipping
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
            GameManager.Instance.PlayerLevelSystem.AddExperience(20);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            GameManager.Instance.PlayerLevelSystem.AddExperience(200);
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
