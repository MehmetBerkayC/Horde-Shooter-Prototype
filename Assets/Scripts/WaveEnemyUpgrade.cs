using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveEnemyUpgrade : MonoBehaviour
{
    private void Start()
    {
        WaveEnemyUpgradeEvent testingEvents = GetComponent<WaveEnemyUpgradeEvent>();
        testingEvents.WavePassed += TestingEvents_WavePassed;
    }

    private void TestingEvents_WavePassed(object sender, System.EventArgs e)
    {
        Debug.Log("Wave Pass bro");
    }
}
