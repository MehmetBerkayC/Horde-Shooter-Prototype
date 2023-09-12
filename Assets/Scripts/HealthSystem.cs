public enum EntityType
{
    Player,
    Monster,
    Boss
}

public class HealthSystem
{
    float _maxHealth;
    float _currentHealth;
    bool _isAlive;

    EntityType _entityType;

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
        private set
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

    public EntityType EntityType
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

    // Constructor
    public HealthSystem(float maxHealth, EntityType entityType)
    {
        MaxHealth = Health = maxHealth;
        EntityType = entityType;
        IsAlive = true;
    }

    public void TakeDamage(float damageAmount)
    {
        if (Health > 0f)
        {
            Health -= damageAmount;
        }
        else
        {
            Health = 0f;
            IsAlive = false;
        }
    }

    public void Heal(float healAmount)
    {

        if (Health < MaxHealth)
        {
            Health += healAmount;
        }

        if (Health > MaxHealth)
        {
            Health = MaxHealth;
        }
    }
}
