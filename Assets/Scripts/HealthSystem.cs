using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem 
{
    float _maxHealth;
    float _currentHealth;
    bool _isAlive;

    public HealthSystem(float maxHealth)
    {
        _maxHealth = _currentHealth = maxHealth;
        _isAlive = true;
    }

    public bool IsAlive()
    {
        return _isAlive;
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;

        if (_currentHealth <= 0f)
        {
            _currentHealth = 0f;
            _isAlive = false;
        }
    }

    public void Heal(float heal)
    {
        _currentHealth += heal;

        if (_currentHealth > _maxHealth)
        {
            _currentHealth = _maxHealth;
        }
    }
}
