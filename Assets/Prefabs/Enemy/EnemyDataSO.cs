using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName ="EnemyDataSO",menuName ="Enemy Data")] 
public class EnemyDataSO : ScriptableObject
{
    public Sprite _sprite;
    public float _baseHealth;
    public float _baseDamage;
    public float _baseSpeed;
    public int _baseEXP;

    private void OnEnable()
    {
        if (EnemySpawner.Instance != null)
        {
            EnemySpawner.Instance.OnWavePassed += Upgrade;
        }
    }

    private void OnDisable()
    {
        if (EnemySpawner.Instance != null)
        {
            EnemySpawner.Instance.OnWavePassed -= Upgrade;    
        }
    }

    public void Upgrade(object sender, EventArgs e)
    {
        _baseHealth += 5;
        _baseSpeed += 1;
        _baseDamage += 2;
    }
}
