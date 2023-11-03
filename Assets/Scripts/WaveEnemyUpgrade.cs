using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveEnemyUpgrade : MonoBehaviour
{
    public GameObject[] _enemyPrefabs;
    private void Start()
    {
        WaveEnemyUpgradeEvent testingEvents = GetComponent<WaveEnemyUpgradeEvent>();
        testingEvents.WavePassed += TestingEvents_WavePassed;
    }

    private void TestingEvents_WavePassed(object sender, System.EventArgs e)
    {
        Debug.Log("Wave Pass bro");
        foreach (var enemy in _enemyPrefabs)
        {
            
        }
    }
    
}