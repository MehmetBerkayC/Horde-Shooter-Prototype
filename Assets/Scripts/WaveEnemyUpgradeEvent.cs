using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveEnemyUpgradeEvent : MonoBehaviour
{
    public event EventHandler WavePassed;

    private void Start()
    {

    }
    private void Update()
    {
        if(EnemySpawner.Instance._WaveStartEnd == true)
        {
            WavePassed?.Invoke(this, EventArgs.Empty);
        }
    }
}
