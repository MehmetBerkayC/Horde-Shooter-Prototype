using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName ="EnemyDataSO",menuName ="Enemy Data")] 
public class EnemyDataSO : ScriptableObject
{
    public Sprite sprite;
    public float Health;
    public float Damage;
    public float Speed;

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
        Health += 5;
        Speed += 1;
        Damage += 2;
    }
}
