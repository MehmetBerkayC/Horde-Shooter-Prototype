public enum Entity
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

    Entity _entityType;

    public HealthSystem(float maxHealth, Entity entityType)
    {
        _maxHealth = _currentHealth = maxHealth;
        _isAlive = true;
        _entityType = entityType;
    }

    public bool IsAlive()
    {
        return _isAlive;
    }

    public virtual void TakeDamage(float damage)
    {
        _currentHealth -= damage;

        if (_currentHealth <= 0f)
        {
            _currentHealth = 0f;
            _isAlive = false;
        }
    }

    public virtual void Heal(float heal)
    {
        _currentHealth += heal;

        if (_currentHealth > _maxHealth)
        {
            _currentHealth = _maxHealth;
        }
    }
}
