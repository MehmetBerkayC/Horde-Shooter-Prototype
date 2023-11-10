using System;
using UnityEngine;

public class HealthSystem
{
    public event EventHandler OnDamaged;
    public event EventHandler OnDead;

    private int _health, _maxHealth;

    public int Health { get => _health; private set => _health = value; }

    public HealthSystem (int maxHealth)
    {
        _maxHealth = maxHealth;
        Health = _maxHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        Health -= damageAmount;

        OnDamaged?.Invoke(this, EventArgs.Empty);

        //Debug.Log("Current Health: " + Health);

        if (Health <= 0)
        {
            OnDead?.Invoke(this, EventArgs.Empty);
        }
    }
}

