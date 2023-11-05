using UnityEngine;

public enum EntityType
{
    Player,
    Monster
}

public class HealthSystem : MonoBehaviour, IDamageable
{
    // Fields
    [SerializeField] float _maxHealth;
    float _currentHealth;
    bool _isAlive;

    [SerializeField] EntityType _entityType;

    // Properties
    public float Health
    {
        get
        {
            return _currentHealth;
        }
        private set
        {
            _currentHealth = value;
        }
    }

    public float MaxHealth
    {
        get
        {
            return _maxHealth;
        }
        set
        {
            _maxHealth = value;
        }
    }

    public bool IsAlive
    {
        get
        {
            return _isAlive;
        }
        private set
        {
            _isAlive = value;
        }
    }

    public EntityType Entity
    {
        get 
        { 
            return _entityType; 
        }
        private set
        {
            _entityType = value;
        }
    }

    // Functions
    public void TakeDamage(float damageAmount)
    {
        Health -= damageAmount;

        if (Health < 0f)
        {
            IsAlive = false;
            Destroy(this.gameObject);
            OnDestroyed();
        }
    }

    public void Heal(float healAmount)
    {
        Health += healAmount;
        Health = Mathf.Clamp(Health, 0, MaxHealth);
    }

    private void OnDestroyed()
    {
        EnemySpawner.Instance.onEnemyKilled();
    }
}

