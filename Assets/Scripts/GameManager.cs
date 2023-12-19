using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public static Player Player; // May want to make Playerscript a singleton instead

    private void Awake()
    {
        // Singleton Pattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {

    }
}
